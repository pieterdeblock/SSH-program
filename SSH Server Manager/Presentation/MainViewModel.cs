using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SSH_Server_Manager.Models;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace SSH_Server_Manager.Presentation
{
    public class MainViewModel : ObservableObject
    {
        public IRelayCommand ConnectCommand { get; private set; }
        public IRelayCommand TestCommand { get; private set; }
        private List<UserData> _userData = new List<UserData>();
        private readonly IFile logic;
        private readonly IShell shell;
        private string remoteclient = "";
        private string user = "";
        private string password = "";
        private string connectionSFTP = "";
        private string connectionSSH = "";
        private string connectionTime = "";
        private TreeView dirTree = new TreeView();
        private DispatcherTimer timer = new DispatcherTimer();
        private Stopwatch _stopwatch = new Stopwatch();
        private int seconds = 0;
        private string connectionHost = "";
        public string RemoteClient
        { 
            get => remoteclient;
            set => SetProperty(ref remoteclient, value);
        }
        public string User
        {
            get => user;
            set => SetProperty(ref user, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public TreeView DirTree
        {
            get => dirTree;
            private set => SetProperty(ref dirTree, value);
        }

        public String ConnectionSFTP { get => connectionSFTP; private set => SetProperty(ref connectionSFTP, value); }
        public String ConnectionSSH { get => connectionSSH; private set => SetProperty(ref connectionSSH, value); }

        public String ConnectionTime { get => connectionTime; private set => SetProperty(ref connectionTime, value); }

        public String ConnectionHost { get => connectionHost; private set => SetProperty(ref connectionHost, value); }



        public MainViewModel(IFile logic, IShell shell)
        {
            this.logic = logic;
            this.shell = shell;
            //_userData = ReadJSONfile();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            ConnectCommand = new RelayCommand(async () => await MakeConections());
            TestCommand = new RelayCommand(MakeTestConnections);
        }
        private List<UserData> ReadJSONfile()
        {
            CheckJSONFile();
            List <UserData> data = new List <UserData>();
            using (StreamReader file = System.IO.File.OpenText("UserData.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                data = (List<UserData>)serializer.Deserialize(file, typeof(List<UserData>));
            }

            return data;
        }
        private async Task WriteToJsonFile(UserData userData)
        {
            CheckJSONFile();
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter("UserData.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, userData);
            }
        }
        private void CheckJSONFile()
        {
            if (!System.IO.File.Exists("UserData.json"))
                System.IO.File.Create("UserData.json");
        }


        private void MakeTestConnections()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            logic.Test(RemoteClient, User, Password);
            shell.Test(RemoteClient, User, Password);
            _stopwatch.Stop();
            ConnectionTime = _stopwatch.Elapsed.ToString();
        }

        private async Task MakeConections()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            timer.Start();
            var t1 = WriteToJsonFile(new UserData { Host = RemoteClient, UserName = User, Password = Password });
            var t2 = logic.MakeSFTPConnection(RemoteClient, User, Password);
            var t3 = shell.MakeSSHConnection(RemoteClient, User, Password);
            await Task.WhenAll(t1, t2, t3);
            ConnectionSFTP = t2.Result;
            ConnectionSSH = t3.Result;
            _stopwatch.Stop();
            timer.Stop();
            ConnectionTime = _stopwatch.Elapsed.ToString();
            shell.TerminalInstance();
            ConnectionHost = shell.Info.ServerVersion;
            //seconds = 0;
            //timer.Stop();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            ConnectionTime = $"Connecting... {seconds++.ToString()}";
        }
    }
}
