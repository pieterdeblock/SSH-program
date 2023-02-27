using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Server_Manager.Models
{
    public interface IFile
    {
        List<String> listFiles(string host, string username, string password, string directory);
        Task<String> MakeSFTPConnection(string host, string username, string password);
        void Test(string host, string username, string password);
    }
}
