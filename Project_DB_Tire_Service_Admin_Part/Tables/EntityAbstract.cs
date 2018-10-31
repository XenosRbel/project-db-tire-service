﻿using System.Data.SqlClient;
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
        public SqlConnection connection;

        public EntityAbstract()
        {
            SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder
            {
                DataSource = "piron.database.windows.net",
                InitialCatalog = "Autocervice",
                PersistSecurityInfo = false,
                UserID = "piron_app",
                Password = "Pavel9684997",
                MultipleActiveResultSets = false,
                Encrypt = false,
                ConnectTimeout = 30
            };

            connection = new SqlConnection(Builder.ToString()/*new Properties.Settings().dbConnectionS*/);
        }

        public virtual async void ExecuteNonQuery(SqlCommand command)
        {
            connection.Open();

            var transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                await command.ExecuteNonQueryAsync();
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
