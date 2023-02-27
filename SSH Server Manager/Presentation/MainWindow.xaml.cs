﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using static SSH_Server_Manager.Presentation.DirectoryItem;

namespace SSH_Server_Manager.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            var viewModel = DataContext as MainViewModel;
            DataContext = vm; 
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel.ConnectCommand.CanExecute(null)) viewModel.ConnectCommand.Execute(null);
            if(viewModel.TestCommand.CanExecute(null)) viewModel.TestCommand.Execute(null);
        }
    }
}
