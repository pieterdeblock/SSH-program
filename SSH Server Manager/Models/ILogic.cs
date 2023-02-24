using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Server_Manager.Models
{
    public interface ILogic
    {
        List<String> listFiles(string host, string username, string password, string directory);
    }
}
