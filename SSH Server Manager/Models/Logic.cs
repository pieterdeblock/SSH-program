using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;

namespace SSH_Server_Manager.Models
{
    class Logic : ILogic
    {
        public List<String> listFiles(string host, string username, string password, string directory)
        {
            using (SftpClient sftp = new SftpClient(host, username, password))
            {
                try
                {
                    sftp.Connect();
                    List<String> files = sftp.ListDirectory(directory).Select(s => s.FullName).ToList();
                    sftp.Disconnect();
                    return files;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
