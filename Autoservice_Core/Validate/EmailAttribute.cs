using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Autoservice_Core.Validate
{
    internal class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute()
        {
        }

        public EmailAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public EmailAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public override bool IsValid(object value)
        {
            string emailPattern = @"([\w.-]+\@([\w-]+)\.+\w{2,})";
            var result = Regex.IsMatch(Convert.ToString(value), emailPattern);
            return result;
        }
    }
}
