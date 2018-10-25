﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_DB_Tire_Service_Admin_Part.Entity.Validate;

namespace Project_DB_Tire_Service_Admin_Part.Tables
{
    [Serializable]
    partial class Customers
    {
        public Customers(int iD, string fIO, string phoneNumber, string email)
        {
            IdCustomer = iD;
            FioC = fIO;
            Phone = phoneNumber;
            Email = email;

            connection = new MySqlConnection(new Properties.Settings().dbConnectionS);
        }

        public int IdCustomer { set; get; }
        public string FioC { set; get; }
        [Phone]
        public string Phone { set; get; }
        [Email]
        public string Email { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Customers customers &&
                   IdCustomer == customers.IdCustomer &&
                   FioC == customers.FioC &&
                   Phone == customers.Phone &&
                   Email == customers.Email;
        }

        public override int GetHashCode()
        {
            var hashCode = -1956427436;
            hashCode = hashCode * -1521134295 + IdCustomer.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FioC);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }
    }
}
