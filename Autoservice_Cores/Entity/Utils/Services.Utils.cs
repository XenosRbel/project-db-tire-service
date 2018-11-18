using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice_Core.Entity
{
    public partial class Services : EntityAbstract
    {
        public Services() : base()
        {
        }

        public override object Select()
        {
            Connection.Open();

            var transaction = Connection.BeginTransaction();
            var command = new SqlCommand(SelectTable(), Connection);
            var customersData = new List<Services>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new Services
                    {
                        IdServices = Convert.ToInt32(reader[0]),
                        NameService = Convert.ToString(reader[1]),
                        Radius = Convert.ToInt32(reader[2]),
                        Price = Convert.ToSingle(reader[3]),
                        ImageBytes = (byte[])reader[4]
                    };

                    customersData.Add(obj);
                }
                reader.Close();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Debug.Print(e.Message);
            }

            Connection.Close();
            return customersData;
        }

        public override void Insert()
        {
            var cmd = new SqlCommand(InsertData());

            cmd.Parameters.AddWithValue("@nameService", this.NameService);
            cmd.Parameters.AddWithValue("@radius", this.Radius);
            cmd.Parameters.AddWithValue("@price", this.Price);
            cmd.Parameters.AddWithValue("@photoDetails", this.ImageBytes);

            ExecuteNonQuery(cmd);
        }

        public override void Delete()
        {
            var cmd = new SqlCommand(DeleteData());
            cmd.Parameters.AddWithValue("@idServices", this.IdServices);

            ExecuteNonQuery(cmd);
        }

        public override void Update()
        {
            Connection.Open();

            var transaction = Connection.BeginTransaction();
            var command = new SqlCommand(UpdateData(), Connection);
            var adapter = new SqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.Parameters.AddWithValue("@idServices", this.IdServices);
                adapter.UpdateCommand.Parameters.AddWithValue("@nameService", this.NameService);
                adapter.UpdateCommand.Parameters.AddWithValue("@radius", this.Radius);
                adapter.UpdateCommand.Parameters.AddWithValue("@price", this.Price);
                adapter.UpdateCommand.Parameters.AddWithValue("@photoDetails", this.ImageBytes);

                adapter.UpdateCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Debug.Print(e.Message);
            }

            Connection.Close();
        }

        string SelectTable()
        {
            return "SELECT idServices, nameService, radius, price, photoDetails FROM Services;";
        }

        string InsertData()
        {
            return "exec Add_Services @nameService, @radius, @price, @photoDetails;";
        }

        string DeleteData()
        {
            return "DELETE FROM Services WHERE (idServices = (@idServices));";
        }

        string UpdateData()
        {
            return "UPDATE Services SET nameService = @nameService, radius = @radius, price = @price, photoDetails = @photoDetails WHERE (idServices = @idServices);";
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
                   // writer.Write(BitmapImageToByte(this.PhotoDetails));
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
                   // obj.PhotoDetails = ConvertBinToImage(reader.ReadBytes(byte.MaxValue));
                }
            }
            return obj;
        }
        #endregion

    }
}
