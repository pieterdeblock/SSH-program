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
        private List<String> subGroups= new List<String>() { "Poes", "Sloeber"};
        private List<String> entries = new List<String>() { "Dogs", "Cats" };
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

        public List<String> SubGroups { get => subGroups ; set => SetProperty(ref subGroups, value); }

        public List<String> Entries { get => entries; set => SetProperty(ref entries, value); }

        public IList<object> Items
        {
            get
            {
                IList<object> childNodes = new List<object>();
                foreach (var group in this.SubGroups)
                    childNodes.Add(group);
                foreach (var entry in this.Entries)
                    childNodes.Add(entry);

                return childNodes;
            }
        }

        public MainViewModel(ILogic logic)
        {
            this.logic = logic;
            ConnectCommand = new RelayCommand(GetFiles);
        }

        private void GetFiles()
        {
            logic.listFiles(RemoteClient, User, Password, "/");
           
        }
    }
}
