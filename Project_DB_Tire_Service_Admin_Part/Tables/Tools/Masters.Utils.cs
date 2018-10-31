using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Masters : EntityAbstract
    {
        public Masters() : base()
        {
        }

        public List<T> Load<T>() where T : Masters, new()
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
                    obj.FIO = reader.GetString(1);
                    obj.Specialization = reader.GetString(2);
                    obj.Phone = reader.GetString(3);

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

            cmd.Parameters.AddWithValue("@fioM", this.FIO);
            cmd.Parameters.AddWithValue("@spec", this.Specialization);
            cmd.Parameters.AddWithValue("@phone", this.Phone);

            ExecuteNonQuery(cmd);
        }

        public override void Delete()
        {
            var cmd = new SqlCommand(DeleteData());
            cmd.Parameters.AddWithValue("@idMaster", ID);

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
                adapter.UpdateCommand.Parameters.AddWithValue("@idMaster", this.ID);
                adapter.UpdateCommand.Parameters.AddWithValue("@fioM", this.FIO);
                adapter.UpdateCommand.Parameters.AddWithValue("@specialization", this.Specialization);
                adapter.UpdateCommand.Parameters.AddWithValue("@phone", this.Phone);

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
            return "SELECT idMaster, fioM, specialization, phone FROM Masters;";
        }

        string InsertData()
        {
            return "exec Add_Masters @fioM, @spec, @phone;";
        }

        string DeleteData()
        {
            return "DELETE FROM Masters WHERE (idMaster = (@idMaster));";
        }

        string UpdateData()
        {
            return "UPDATE Masters SET fioM = @fioM, phone = @phone, email = @email WHERE (idMaster = @idMaster);";
        }

        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(this.ID);
                    writer.Write(this.FIO);
                    writer.Write(this.Specialization);
                    writer.Write(this.Phone);
                }
                return m.ToArray();
            }
        }

        public static T Desserialize<T>(byte[] data) where T : Masters, new()
        {
            T obj = new T();

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    obj.ID = reader.ReadInt32();
                    obj.FIO = reader.ReadString();
                    obj.Specialization = reader.ReadString();
                    obj.Phone = reader.ReadString();
                }
            }
            return obj;
        }
    }
}
