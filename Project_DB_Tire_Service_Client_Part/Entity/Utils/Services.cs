using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Entity
{
    partial class Services : EntityAbstract
    {
        public override object Select()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(SelectTable(), connection);
            var customersData = new List<Services>();
            
            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new Services()
                    {
                        IdServices = reader.GetInt32(0),
                        NameService = reader.GetString(1),
                        Radius = reader.GetInt32(2),
                        Price = reader.GetFloat(3),
                        ImageByte = new byte[reader.GetBytes(reader.GetOrdinal("photoDetails"), 0, null, 0, int.MaxValue)]
                    };
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

        string SelectTable()
        {
            return "SELECT idServices, nameService, radius, price, photoDetails FROM Services;";
        }

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
                    writer.Write(this.ImageByte);
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
                    obj.ImageByte = reader.ReadBytes(byte.MaxValue);
                }
            }
            return obj;
        }
    }
}