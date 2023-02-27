using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Server_Manager.Models
{
    public class UserData
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserData() { }
    }
}
