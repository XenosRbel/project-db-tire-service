using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservice_Core.Validate;

namespace Autoservice_Core.Entity
{
    [Serializable]
    public partial class Customers : EntityAbstract
    {
        public Customers(int iD, string fIO, string phoneNumber, string email) : base()
        {
            IdCustomer = iD;
            FioC = fIO;
            Phone = phoneNumber;
            Email = email;
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
