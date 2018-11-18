using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice_Core.Entity
{
    public abstract class EntityAbstract
    {
        public SqlConnection Connection { get; set; }

        public SqlConnectionStringBuilder Builder { get; set; }

        public EntityAbstract()
        {
            Builder = new SqlConnectionStringBuilder
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

            Connection = new SqlConnection(Builder.ToString()/*new Properties.Settings().dbConnectionS*/);
        }

        public virtual async void ExecuteNonQuery(SqlCommand command)
        {
            Connection.Open();

            var transaction = Connection.BeginTransaction();
            command.Connection = Connection;
            command.Transaction = transaction;

            try
            {
                await command.ExecuteNonQueryAsync();
                transaction.Commit();
                Connection.Close();
            }
            catch (Exception)
            {
                transaction.Rollback();
                Connection.Close();
            }
        }

        public virtual object Select() { return null; }
        public virtual void Insert() { }
        public virtual void Update() { }
        public virtual void Delete() { }
    }
}
