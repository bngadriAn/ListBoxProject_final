using _2025_01_23WPF.Views;
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
using System.Windows.Shapes;

namespace _2025_01_23WPF
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView_Click(sender, e);
        }

        private void AddView_Click(object sender, RoutedEventArgs e)
        {
            var addView = new AddView();
            MainFrame.Content = addView;
        }

        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            var lbView = new ListboxView();
            MainFrame.Content = lbView;
        }
    }
}
