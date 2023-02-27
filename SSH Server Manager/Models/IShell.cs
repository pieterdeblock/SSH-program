using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Server_Manager.Models
{
    public interface IShell
    {
        Task<String> MakeSSHConnection(string host, string username, string password);
        void Test(string host, string username, string password);
        void TerminalInstance();

        ConnectionInfo Info { get; }
    }
}
