using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Constants
{
    public class EmailSendingConstants
    {
        public static readonly string EmailAddress = Environment.GetEnvironmentVariable("EmailAddress");
        public static readonly string Password = Environment.GetEnvironmentVariable("Password");
    }
}
