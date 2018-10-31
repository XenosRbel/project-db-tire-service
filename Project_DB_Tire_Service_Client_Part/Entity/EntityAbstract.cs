using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project_DB_Tire_Service_Client_Part.Entity
{
    abstract class EntityAbstract
    {
        [NonSerialized]
        public MySqlConnection connection;

        public EntityAbstract()
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder
            {
                Server = "192.168.0.12",
                Database = "Autoservice",
                UserID = "pavel",
                Password = "pavel1998",
                Port = 3306,
                ConnectionTimeout = 30,
                SqlServerMode = false,
            };

            connection = new MySqlConnection(Builder.ToString());
        }

        public virtual async void ExecuteNonQuery(MySqlCommand command)
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

        public virtual object Select() {return null;}
        public virtual void Insert() { }
        public virtual void Update() { }
        public virtual void Delete() { }
    }
}