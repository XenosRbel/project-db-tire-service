using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Customers : EntityAbstract
    {
        public Customers() : base()
        {
        }

        public List<Customers> Load()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(SelectCustomersTable(), connection);
            var customersData = new List<Customers>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customersData.Add(
                        new Customers(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3)
                            )
                        );
                }
                reader.Close();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            connection.Close();
            return customersData;
        }

        public void DeleteCustomer() {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(DeleteCustomerData(), connection);
            var adapter = new SqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.Parameters.AddWithValue("@idCustomer", this.IdCustomer);

                adapter.DeleteCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            connection.Close();
        }

        public void UpdateCustomer() {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(UpdateCustomerData(), connection);
            var adapter = new SqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.Parameters.AddWithValue("@idCustomer", this.IdCustomer);
                adapter.UpdateCommand.Parameters.AddWithValue("@fioC", this.FioC);
                adapter.UpdateCommand.Parameters.AddWithValue("@phone", this.Phone);
                adapter.UpdateCommand.Parameters.AddWithValue("@email", this.Email);

                adapter.UpdateCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            connection.Close();
        }

        public override void Insert()
        {
            var cmd = new SqlCommand(InsertCustomerData());

            cmd.Parameters.AddWithValue("@fioC", this.FioC);
            cmd.Parameters.AddWithValue("@phone", this.Phone);
            cmd.Parameters.AddWithValue("@email", this.Email);

            ExecuteNonQuery(cmd);
        }

        string SelectCustomersTable()
        {
            return "SELECT idCustomer, fioC, phone, email FROM Customers;";
        }

        string InsertCustomerData() {
            return "exec Add_Customers @fioC, @phone, @email;"; 
        }

        string DeleteCustomerData() {
            return "DELETE FROM Customers WHERE (idCustomer = (@idCustomer));";
        }

        string UpdateCustomerData() {
            return "UPDATE Customers SET fioC = @fioC, phone = @phone, email = @email WHERE (idCustomer = @idCustomer);";
        }

        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(this.IdCustomer);
                    writer.Write(this.FioC);
                    writer.Write(this.Phone);
                    writer.Write(this.Email);
                }
                return m.ToArray();
            }
        }

        public static T Desserialize<T>(byte[] data) where T : Customers, new()
        {
            T obj = new T();

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    obj.IdCustomer = reader.ReadInt32();
                    obj.FioC = reader.ReadString();
                    obj.Phone = reader.ReadString();
                    obj.Email = reader.ReadString();
                }
            }
            return obj;
        }
    }
}
