using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Autoservice_Core.Entity.Utils;

namespace Autoservice_Core.Entity
{
    public partial class Armor : EntityAbstract
    {
        public Armor() : base()
        {
        }

        public override object Select()
        {
            Connection.Open();
            var transaction = Connection.BeginTransaction();
            var command = new SqlCommand(SelectTable(), Connection);
            var data = new List<Armor>();

            command.Transaction = transaction;

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new Armor()
                    {
                        IdArmor = Convert.ToInt32(reader[0]),
                        ArrivalDate = Convert.ToDateTime(reader[1]),
                        Customer = Convert.ToString(reader[2]),
                        NameService = Convert.ToString(reader[3]),
                        StatusA = (ArmorStatus)Convert.ToByte(reader[4]),
                        DateExecution = Convert.ToDateTime(reader[5])
                    };

                    data.Add(obj);
                }
                reader.Close();
               
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Debug.Print($"Class name:{this.GetType().Name}\n Exception:{e.Message}\n Method:{e.StackTrace}");
            }

            Connection.Close();
            return data;
        }

        public override void Insert()
        {
            var cmd = new SqlCommand(InsertData());

            cmd.Parameters.AddWithValue("@arrivalDate", this.ArrivalDate);
            cmd.Parameters.AddWithValue("@customerUser", this.Customer);
            cmd.Parameters.AddWithValue("@service", this.NameService);
            cmd.Parameters.AddWithValue("@statusA", this.StatusA);
            cmd.Parameters.AddWithValue("@dateExecution", this.DateExecution);

            ExecuteNonQuery(cmd);
        }

        public override void Delete()
        {
            var cmd = new SqlCommand(DeleteData());
            cmd.Parameters.AddWithValue("@id", IdArmor);

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
                adapter.UpdateCommand.Parameters.AddWithValue("@arrivalDate", this.ArrivalDate);
                adapter.UpdateCommand.Parameters.AddWithValue("@idCustomer", this.Customer);
                adapter.UpdateCommand.Parameters.AddWithValue("@idServices", this.NameService);
                adapter.UpdateCommand.Parameters.AddWithValue("@statusA", (byte)this.StatusA);
                adapter.UpdateCommand.Parameters.AddWithValue("@dateExecution", this.DateExecution);

                adapter.UpdateCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Debug.Print($"Class name:{this.GetType().Name}\n Exception:{e.Message}\n Method:{e.StackTrace}");
            }

            Connection.Close();
        }

        string SelectTable()
        {
            return "SELECT id, arrivalDate, fioC, nameService, statusA, dateExecution FROM Armor " +
                   "INNER JOIN Customers ON Armor.idCustomer = Customers.idCustomer " +
                   "INNER JOIN Services ON Armor.idServices = Services.idServices;";
        }

        string InsertData()
        {
            return "exec Add_Armor @arrivalDate, @customerUser, @service, @statusA, @dateExecution;";
        }

        string DeleteData()
        {
            return "DELETE FROM Armor WHERE (id = (@id));";
        }

        string UpdateData()
        {
            return "UPDATE Armor SET " +
                "arrivalDate = @arrivalDate, idCustomer = @idCustomer, idServices = @idServices, statusA = @statusA, dateExecution = @dateExecution WHERE (id = @id);";
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
                    obj.Customer = reader.ReadString();
                    obj.StatusA = (ArmorStatus)reader.ReadByte();
                    obj.DateExecution = DateTime.FromBinary(reader.ReadInt64());
                }
                stream.Dispose();
            }
            return obj;
        }
    }
}
