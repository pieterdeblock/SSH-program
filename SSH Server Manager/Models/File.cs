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
    class File : IFile
    {
        private SftpClient sftp;
        public string WorkingDirectory { get => sftp.WorkingDirectory;}

        public async Task<String> MakeSFTPConnection(string host, string username, string password)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.sftp = new SftpClient(host, username, password);
                    sftp.Connect();
                    var funtime = sftp.ConnectionInfo;
                });
            }
            catch (Exception ex) { return ($"Connection failed: {ex.Message}"); }
            return ("Connection was successful");
        }
        public void Test(string host, string username, string password)
        {
            try
            {
                this.sftp = new SftpClient(host, username, password);
                sftp.Connect();
            }catch (Exception ex) { }
        }
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
        public void DownloadFile(string remoteSource, string LocalDestination)
        {
            //sftp.DownloadFile
        }
    }
}
