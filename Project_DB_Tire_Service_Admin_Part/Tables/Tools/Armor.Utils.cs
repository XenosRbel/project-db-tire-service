using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Armor : EntityAbstract
    {
        public Armor() : base()
        {
        }

        public List<T> Load<T>() where T : Armor, new()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(SelectTable(), connection);
            var data = new List<T>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    T obj = new T
                    {
                        ID = reader.GetInt32(0),
                        ArrivalDate = reader.GetDateTime(1),
                        IdCustomers = reader.GetInt32(2),
                        IDService = reader.GetInt32(3),
                        StatusA = (ArmorStatus)reader.GetByte(4),
                        DateExecution = reader.GetDateTime(5)
                    };

                    data.Add(obj);
                }
                reader.Close();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            connection.Close();
            return data;
        }

        public override void Insert()
        {
            var cmd = new SqlCommand(InsertData());

            cmd.Parameters.AddWithValue("@arrivalDate", this.ArrivalDate);
            cmd.Parameters.AddWithValue("@customerUser", this.Customer);
            cmd.Parameters.AddWithValue("@service", this.Service);
            cmd.Parameters.AddWithValue("@statusA", this.StatusA);
            cmd.Parameters.AddWithValue("@dateExecution", this.DateExecution);

            ExecuteNonQuery(cmd);
        }

        public override void Delete()
        {
            var cmd = new SqlCommand(DeleteData());
            cmd.Parameters.AddWithValue("@id", ID);

            ExecuteNonQuery(cmd);
        }

        public override void Update()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(UpdateData(), connection);
            var adapter = new SqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.Parameters.AddWithValue("@arrivalDate", this.ArrivalDate);
                adapter.UpdateCommand.Parameters.AddWithValue("@idCustomer", this.IdCustomers);
                adapter.UpdateCommand.Parameters.AddWithValue("@idServices", this.IDService);
                adapter.UpdateCommand.Parameters.AddWithValue("@statusA", (byte)this.StatusA);
                adapter.UpdateCommand.Parameters.AddWithValue("@dateExecution", this.DateExecution);

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
            return "SELECT id, arrivalDate, idCustomer, idServices, statusA, dateExecution FROM Armor;";
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
                    writer.Write(this.ID);
                    writer.Write(this.ArrivalDate.ToBinary());
                    writer.Write(this.IdCustomers);
                    writer.Write(this.IDService);
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
                    obj.ID = reader.ReadInt32();
                    obj.ArrivalDate = DateTime.FromBinary(reader.ReadInt64());
                    obj.IdCustomers = reader.ReadInt32();
                    obj.IDService = reader.ReadInt32();
                    obj.StatusA = (ArmorStatus)reader.ReadByte();
                    obj.DateExecution = DateTime.FromBinary(reader.ReadInt64());
                }
                stream.Dispose();
            }
            return obj;
        }
    }
}
