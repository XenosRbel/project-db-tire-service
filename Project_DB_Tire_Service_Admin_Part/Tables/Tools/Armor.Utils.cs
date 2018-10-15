using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Armor
    {
        [NonSerialized]
        private MySqlConnection connection;

        public Armor()
        {
            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public List<T> Load<T>() where T : Armor, new()
        {
            connection.Close();
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(SelectTable(), connection);
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

        public void Delete()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(DeleteData(), connection);
            var adapter = new MySqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.Parameters.AddWithValue("@id", ID);

                adapter.DeleteCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            connection.Close();
        }

        public void Update()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(UpdateData(), connection);
            var adapter = new MySqlDataAdapter();

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
            return "SELECT id, arrivalDate, idCustomer, idServices, statusA, dateExecution FROM armor;";
        }

        string InsertData()
        {
            return "INSERT INTO armor (arrivalDate, idCustomer, idServices, statusA, dateExecution) VALUES (@arrivalDate, @idCustomer, @idServices, @statusA, @dateExecution);";
        }

        string DeleteData()
        {
            return "DELETE FROM armor WHERE (id = (@id));";
        }

        string UpdateData()
        {
            return "UPDATE armor SET " +
                "arrivalDate = @arrivalDate, idCustomer = @idCustomer, idServices = @idServices, statusA = @statusA, dateExecution = @dateExecution WHERE (id = @id);";
        }

        private MySqlDataAdapter InsertAdapter(Armor customers)
        {
            var command = new MySqlCommand(InsertData(), connection);
            var adapter = new MySqlDataAdapter();
            adapter.InsertCommand = command;
            adapter.InsertCommand.Parameters.AddWithValue("@fioM", customers.DateExecution);

            return adapter;
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
