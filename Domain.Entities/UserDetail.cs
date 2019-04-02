using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserDetail
    {
        public int userId { get; set; }

        public int userRole { get; set; }

        public string name { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string emailId { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string phoneNumber { get; set; }

        public string userRoleName { get; set; }

    }
}
