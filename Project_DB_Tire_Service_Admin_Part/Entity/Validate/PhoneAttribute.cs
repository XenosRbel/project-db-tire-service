using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneNumbers;

namespace Project_DB_Tire_Service_Admin_Part.Entity.Validate
{
    class PhoneAttribute : ValidationAttribute
    {
        public PhoneAttribute()
        {
        }

        public PhoneAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public PhoneAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public override bool IsValid(object value)
        {
            var phoneNumber = Convert.ToString(value);
            var result = PhoneNumberUtil.IsViablePhoneNumber(phoneNumber);
            return result;
        }
    }
}
