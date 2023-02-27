using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace SSH_Server_Manager.Models
{
    internal class Shell : IShell
    {
        private SshClient _client;

        public ConnectionInfo Info { get => _client.ConnectionInfo; }

        public async Task<String> MakeSSHConnection(string host, string username, string password)
        {
            try
            {
                await Task.Run(() =>
                {
                    _client = new SshClient(host, username, password);
                    _client.Connect();
                });
            } catch (Exception ex) { return $"Connection Failed: {ex.Message}"; }
            return "Connection was successful";
        }
        public void Test(string host, string username, string password)
        {
            try
            {
                _client = new SshClient(host, username, password);
                _client.Connect();
            } catch(Exception ex) { }
        }
        public void TerminalInstance()
        {
            string test = "";
            ShellStream shellStreamSSH = _client.CreateShellStream("vt-100", 80, 60, 800, 600, 65536);
            shellStreamSSH.Write("pwd" + "\n");
            test = shellStreamSSH.Read();
            //shellStreamSSH.
            

        }
    }
}
