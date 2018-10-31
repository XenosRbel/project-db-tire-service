using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Orders : EntityAbstract
    {
        public Orders() : base() {
        }

        public List<T> Load<T>() where T : Orders, new()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(SelectTable(), connection);
            var customersData = new List<T>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    T obj = new T();
                    obj.ID = reader.GetInt32(0);
                    obj.IdMaster = reader.GetString(1);
                    obj.OrderDate = reader.GetDateTime(4);
                    obj.IdServices = reader.GetString(2);
                    obj.IdCustomer = reader.GetString(4);
                    obj.CountO = reader.GetInt32(5);

                    customersData.Add(obj);
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

        public override void Insert()
        {
            var cmd = new SqlCommand(InsertData());

            cmd.Parameters.AddWithValue("@idMaster", this.IdMaster);
            cmd.Parameters.AddWithValue("@orderDate", this.OrderDate);
            cmd.Parameters.AddWithValue("@idServices", this.IdServices);
            cmd.Parameters.AddWithValue("@idCustomer", this.IdCustomer);
            cmd.Parameters.AddWithValue("@countO", this.CountO);

            ExecuteNonQuery(cmd);
        }

        public override void Delete()
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new SqlCommand(DeleteData(), connection);
            var adapter = new SqlDataAdapter();

            command.Transaction = transaction;

            try
            {
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.Parameters.AddWithValue("@idOrder", ID);

                adapter.DeleteCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            connection.Close();
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
                adapter.UpdateCommand.Parameters.AddWithValue("@idOrder", this.ID);
                adapter.UpdateCommand.Parameters.AddWithValue("@idMaster", this.IdMaster);
                adapter.UpdateCommand.Parameters.AddWithValue("@orderDate", this.OrderDate);
                adapter.UpdateCommand.Parameters.AddWithValue("@idServices", this.IdServices);
                adapter.UpdateCommand.Parameters.AddWithValue("@idCustomer", this.IdCustomer);
                adapter.UpdateCommand.Parameters.AddWithValue("@countO", this.CountO);

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
            return "select idOrder, fioM, nameService, fioC, orderDate, countO from SelectOrder;";
        }

        string InsertData()
        {
            return "exec Add_Orders @idMaster, @orderDate, @idServices, @idCustomer, @countO;";
        }

        string DeleteData()
        {
            return "DELETE FROM Orders WHERE (idOrder = (@idOrder));";
        }

        string UpdateData()
        {
            return "UPDATE Orders SET " +
                "idMaster = @idMaster, orderDate = @orderDate, idServices = @idServices, idCustomer = @idCustomer, countO = @countO" +
                " WHERE (idOrder = @idOrder);";
        }

        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(this.ID);
                    writer.Write(this.IdMaster);
                    writer.Write(this.OrderDate.ToBinary());
                    writer.Write(this.IdServices);
                    writer.Write(this.IdCustomer);
                    writer.Write(this.CountO);
                }
                return m.ToArray();
            }
        }

        public static T Desserialize<T>(byte[] data) where T : Orders, new()
        {
            T obj = new T();

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    obj.ID = reader.ReadInt32();
                    obj.IdMaster = reader.ReadString();
                    obj.OrderDate = DateTime.FromBinary(reader.ReadInt64());
                    obj.IdServices = reader.ReadString();
                    obj.IdCustomer = reader.ReadString();
                    obj.CountO = reader.ReadInt32();
                }
            }
            return obj;
        }
    }
}
