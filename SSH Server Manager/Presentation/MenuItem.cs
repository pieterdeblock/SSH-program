﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SSH_Server_Manager.Presentation
{
    public class MenuItem
    {
        public string Title { get; set; }
        public ObservableCollection<MenuItem> Items { get; set; }


        public MenuItem() 
        {
            Items = new ObservableCollection<MenuItem>();
        }
    }
}
