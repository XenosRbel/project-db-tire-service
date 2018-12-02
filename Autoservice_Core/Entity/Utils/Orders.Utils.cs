using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice_Core.Entity
{
    public partial class Orders : EntityAbstract
    {
        public Orders() : base() {
        }

        public override object Select()
        {
            Connection.Open();

            var transaction = Connection.BeginTransaction();
            var command = new SqlCommand(SelectTable(), Connection);
            var customersData = new List<Orders>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    var obj = new Orders();
                    obj.ID = Convert.ToInt32(reader[0]);
                    obj.IdMaster = Convert.ToString(reader[1]);
                    obj.OrderDate = Convert.ToDateTime(reader[4]);
                    obj.IdServices = Convert.ToString(reader[2]);
                    obj.IdCustomer = Convert.ToString(reader[3]);
                    obj.CountO = Convert.ToInt32(reader[5]);

                    customersData.Add(obj);
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
            var cmd = new SqlCommand(DeleteData());
            cmd.Parameters.AddWithValue("@idOrder", ID);

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
                adapter.UpdateCommand.Parameters.AddWithValue("@idOrder", this.ID);
                adapter.UpdateCommand.Parameters.AddWithValue("@idMaster", this.IdMaster);
                adapter.UpdateCommand.Parameters.AddWithValue("@orderDate", this.OrderDate);
                adapter.UpdateCommand.Parameters.AddWithValue("@idServices", this.IdServices);
                adapter.UpdateCommand.Parameters.AddWithValue("@idCustomer", this.IdCustomer);
                adapter.UpdateCommand.Parameters.AddWithValue("@countO", this.CountO);

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
