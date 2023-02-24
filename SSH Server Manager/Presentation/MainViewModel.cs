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

namespace SSH_Server_Manager.Presentation
{
    public class MainViewModel : ObservableObject
    {
        public IRelayCommand ConnectCommand { get; private set; }
        private readonly ILogic logic;
        private string remoteclient = "";
        private string user = "";
        private string password = "";
        private MenuItem dirTree = new MenuItem();
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
        public MenuItem DirTree
        {
            get => dirTree;
            private set => SetProperty(ref dirTree, value);
        }

        public MainViewModel(ILogic logic)
        {
            this.logic = logic;
            ConnectCommand = new RelayCommand(GetFiles);
        }

        private void GetFiles()
        {
            logic.listFiles(RemoteClient, User, Password, "/");
            DirTree = new MenuItem() { Title = "Menu" };
            foreach (var item in logic.listFiles(RemoteClient, User, Password, "/"))
                DirTree.Items.Add(new MenuItem() { Title = item });
        }
    }
}
public class MenuItem
{
    public MenuItem()
    {
        this.Items = new ObservableCollection<MenuItem>();
    }

    public string Title { get; set; }

    public ObservableCollection<MenuItem> Items { get; set; }
}
