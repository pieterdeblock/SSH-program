using System;
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
            List<FileTree> files = new List<FileTree> { new FileTree { Children = "child1", ParentFolder = "parent1" }, new FileTree { Children = "child2", ParentFolder = "parent2" } };
            treeView.ItemsSource = files;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel.ConnectCommand.CanExecute(null)) viewModel.ConnectCommand.Execute(null);
            if(viewModel.TestCommand.CanExecute(null)) viewModel.TestCommand.Execute(null);
        }
    }
}
