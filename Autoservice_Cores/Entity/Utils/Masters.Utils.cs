using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice_Core.Entity
{
    public partial class Masters : EntityAbstract
    {
        public Masters() : base()
        {
        }

        public override object Select()
        {
            Connection.Open();

            var transaction = Connection.BeginTransaction();
            var command = new SqlCommand(SelectTable(), Connection);
            var customersData = new List<Masters>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = new Masters();

                    obj.ID = Convert.ToInt32(reader[0]);
                    obj.FIO = Convert.ToString(reader[1]);
                    obj.Specialization = Convert.ToString(reader[2]);
                    obj.Phone = Convert.ToString(reader[3]);

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
            Connection.Open();

            var transaction = Connection.BeginTransaction();
            var command = new SqlCommand(UpdateData(), Connection);
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
            catch (Exception e)
            {
                transaction.Rollback();
                Debug.Print(e.Message);
            }

            Connection.Close();
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
