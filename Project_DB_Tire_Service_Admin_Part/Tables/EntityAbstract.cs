using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    abstract class EntityAbstract
    {
        [NonSerialized]
        private MySqlConnection connection;

        public EntityAbstract()
        {
            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public virtual void ExecuteNonQuery(MySqlCommand command)
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
            }
            catch (Exception)
            {
                transaction.Rollback();
                connection.Close();
            }
        }

        public virtual void Insert() { }
        public virtual void Update() { }
        public virtual void Delete() { }
    }
}
