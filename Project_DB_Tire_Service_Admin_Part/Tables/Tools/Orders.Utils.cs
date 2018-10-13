﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    partial class Orders
    {
        private MySqlConnection connection;

        public Orders()
        {
            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public List<T> Load<T>() where T : Orders, new()
        {
            connection.Close();
            connection.Open();

            var transaction = connection.BeginTransaction();
            var command = new MySqlCommand(SelectTable(), connection);
            var customersData = new List<T>();

            command.Transaction = transaction;
            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    T obj = new T();
                    obj.ID = reader.GetInt32(0);
                    obj.IdMaster = reader.GetInt32(1);
                    obj.OrderDate = reader.GetDateTime(2);
                    obj.IdServices = reader.GetInt32(3);
                    obj.IdCustomer = reader.GetInt32(4);
                    obj.CountO = reader.GetInt32(5);

                    customersData.Add(obj);
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
            return customersData;
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
            return "SELECT idOrder, idMaster, orderDate, idServices, idCustomer, countO FROM orders;";
        }

        string InsertData()
        {
            return "INSERT INTO orders (idMaster, orderDate, idServices, idCustomer, countO) " +
                "VALUES (@idMaster, @orderDate, @idServices, @idCustomer, @countO);";
        }

        string DeleteData()
        {
            return "DELETE FROM orders WHERE (idOrder = (@idOrder));";
        }

        string UpdateData()
        {
            return "UPDATE orders SET " +
                "idMaster = @idMaster, orderDate = @orderDate, idServices = @idServices, idCustomer = @idCustomer, countO = @countO" +
                " WHERE (idOrder = @idOrder);";
        }

        private MySqlDataAdapter InsertAdapter(Services customers)
        {
            var command = new MySqlCommand(InsertData(), connection);
            var adapter = new MySqlDataAdapter();

            adapter.InsertCommand = command;
            adapter.InsertCommand.Parameters.AddWithValue("@nameService", customers.NameService);

            return adapter;
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
                    obj.IdMaster = reader.ReadInt32();
                    obj.OrderDate = DateTime.FromBinary(reader.ReadInt64());
                    obj.IdServices = reader.ReadInt32();
                    obj.IdCustomer = reader.ReadInt32();
                    obj.CountO = reader.ReadInt32();
                }
            }
            return obj;
        }
    }
}
