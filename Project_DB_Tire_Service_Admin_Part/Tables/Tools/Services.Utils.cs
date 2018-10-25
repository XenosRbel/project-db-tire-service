using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Services : EntityAbstract
    {
        [NonSerialized]
        private MySqlConnection connection;

        public Services()
        {
            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public List<T> Load<T>() where T : Services, new()
        {
            connection.Close();
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(SelectTable(), connection);
            var customersData = new List<T>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    T obj = new T();
                    var img = (byte[])reader["photoDetails"];
                    obj.IdServices = reader.GetInt32(0);
                    obj.NameService = reader.GetString(1);
                    obj.Radius = reader.GetInt32(2);
                    obj.Price = reader.GetFloat(3);
                    obj.PhotoDetails = ConvertBinToImage((byte[])reader["photoDetails"]);

                    byte[] buffer = new byte[reader.GetBytes(reader.GetOrdinal("photoDetails"), 0, null, 0, int.MaxValue)];

                    customersData.Add(obj);
                }

                connection.Close();
                connection.Open();

                transaction.Commit();
            }
            catch (Exception)
            {
                connection.Close();
                connection.Open();
                transaction.Rollback();
            }

            connection.Close();
            return customersData;
        }

        public override void Insert()
        {
            var cmd = new MySqlCommand(InsertData());

            cmd.Parameters.AddWithValue("@nameService", this.NameService);
            cmd.Parameters.AddWithValue("@radius", this.Radius);
            cmd.Parameters.AddWithValue("@price", this.Price);
            cmd.Parameters.AddWithValue("@photoDetails", ConvertImageBin(this.SImage));

            ExecuteNonQuery(cmd);
        }

        public override void Delete()
        {
            var cmd = new MySqlCommand(DeleteData());
            cmd.Parameters.AddWithValue("@idServices", this.IdServices);

            ExecuteNonQuery(cmd);
        }

        public override void Update()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(UpdateData(), connection);
            var adapter = new MySqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.Parameters.AddWithValue("@idServices", this.IdServices);
                adapter.UpdateCommand.Parameters.AddWithValue("@nameService", this.NameService);
                adapter.UpdateCommand.Parameters.AddWithValue("@radius", this.Radius);
                adapter.UpdateCommand.Parameters.AddWithValue("@price", this.Price);
                adapter.UpdateCommand.Parameters.AddWithValue("@photoDetails", this.PhotoDetails);

                adapter.UpdateCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            connection.Close();
        }

        string SelectTable()
        {
            return "SELECT idServices, nameService, radius, price, photoDetails FROM services;";
        }

        string InsertData()
        {
            return "call Add_Services(@nameService, @radius, @price, @photoDetails);";
        }

        string DeleteData()
        {
            return "DELETE FROM services WHERE (idServices = (@idServices));";
        }

        string UpdateData()
        {
            return "UPDATE services SET nameService = @nameService, radius = @radius, price = @price, photoDetails = @photoDetails WHERE (idServices = @idServices);";
        }

        #region Serialize
        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(this.IdServices);
                    writer.Write(this.NameService);
                    writer.Write(this.Radius);
                    writer.Write(this.Price);
                    writer.Write(BitmapImageToByte(this.PhotoDetails));
                }
                return m.ToArray();
            }
        }

        public static T Desserialize<T>(byte[] data) where T : Services, new()
        {
            T obj = new T();

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    obj.IdServices = reader.ReadInt32();
                    obj.NameService = reader.ReadString();
                    obj.Radius = reader.ReadByte();
                    obj.Price = (float)reader.ReadDecimal();
                    obj.PhotoDetails = ConvertBinToImage(reader.ReadBytes(byte.MaxValue));
                }
            }
            return obj;
        }
        #endregion

        #region BitmapConventer
        private static byte[] ConvertImageBin(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static byte[] BitmapImageToByte(BitmapImage imageSource)
        {
            var stream = imageSource.StreamSource;

            byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((int)stream.Length);
                }
            }

            return buffer;
        }

        private static BitmapImage ConvertBinToImage(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            var image = new BitmapImage();

            using (var mem = new MemoryStream(data))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }

            image.Freeze();

            return image;
        }
        #endregion

    }
}
