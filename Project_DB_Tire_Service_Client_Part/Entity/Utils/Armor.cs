using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    partial class Armor : EntityAbstract
    {
        public override void Delete()
        {
            base.Delete();
        }

        public override object Select()
        {
            connection.Open();
            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(SelectTable(), connection);
            var data = new List<Armor>();

            command.Transaction = transaction;

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new Armor()
                    {
                        IdArmor = reader.GetInt32(0),
                        ArrivalDate = reader.GetDateTime(1),
                        Customer = reader.GetString(2),
                        NameService = reader.GetString(3),
                        StatusA =  (ArmorStatus)reader.GetByte(4),
                        DateExecution = reader.GetDateTime(5)
                    };

                    data.Add(obj);
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
            return data;
        }

        public override void Insert()
        {
            var cmd = new MySqlCommand(InsertData());

            cmd.Parameters.AddWithValue("@arrivalDate", this.ArrivalDate);
            cmd.Parameters.AddWithValue("@customerUser", this.Customer);
            cmd.Parameters.AddWithValue("@service", this.NameService);
            cmd.Parameters.AddWithValue("@statusA", this.StatusA);
            cmd.Parameters.AddWithValue("@dateExecution", this.DateExecution);

            ExecuteNonQuery(cmd);
        }

        public override void Update()
        {
            base.Update();
        }
        
        string SelectTable()
        {
            return "SELECT id, arrivalDate, idCustomer, idServices, statusA, dateExecution FROM Armor;";
        }

        string InsertData()
        {
            return "call Add_Armor(@arrivalDate, @customerUser, @service, @statusA, @dateExecution);";
        }

        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(this.IdArmor);
                    writer.Write(this.ArrivalDate.ToBinary());
                    writer.Write(this.Customer);
                    writer.Write(this.NameService);
                    writer.Write((byte)this.StatusA);
                    writer.Write(this.DateExecution.ToBinary());
                }
                return m.ToArray();
            }
        }

        public static T Desserialize<T>(byte[] data) where T : Armor, new()
        {
            T obj = new T();

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    obj.IdArmor = reader.ReadInt32();
                    obj.ArrivalDate = DateTime.FromBinary(reader.ReadInt64());
                    obj.Customer = reader.ReadString();
                    obj.NameService = reader.ReadString();
                    obj.StatusA = (ArmorStatus)reader.ReadByte();
                    obj.DateExecution = DateTime.FromBinary(reader.ReadInt64());
                }
                stream.Dispose();
            }
            return obj;
        }
    }
}