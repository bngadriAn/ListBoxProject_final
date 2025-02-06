using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace _2025_01_23WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FilePath = "todo.csv";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void SaveItem()
        {
            var operation = new List<string>();
            foreach (TodoItem item in LB.Items)
            {
                operation.Add(item.ToCsv());
            }
            File.WriteAllLines(FilePath, operation);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    LB.Items.Add(TodoItem.FromCsv(line));
                }
            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (LB.SelectedItem != null)
            {
                var selectedItem = (TodoItem)LB.SelectedItem;
                selectedItem.Deadline = DP.SelectedDate;
                selectedItem.Value = TBInput.Text;
            }
            else
            {
                var item = new TodoItem
                {
                    Value = TBInput.Text,
                    Deadline = DP.SelectedDate,
                    CompletedAt = CB.IsChecked == true ? DateTime.Now : null
                };
                LB.Items.Add(item);
            }
            TBInput.Clear();
            CB.IsChecked = false;
            DP.SelectedDate = null;
            SaveItem();
        }

        private void DelBTN_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you really want to delete the selected item?","Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                LB.Items.Remove(LB.SelectedItem);
                SaveItem();
            }
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TBSearch.Text != "")
            {
                if (LB.HasItems)
                {
                    LB.Items.Clear();
                    LB.Items.Refresh();
                }
                string searchQuery = TBSearch.Text;
                IEnumerable<string> FilteringQuery =
                    from item in 
                    where item.StartsWith(searchQuery) || item.Contains(searchQuery)
                    orderby item ascending
                    select item;
                //LB.Items.Refresh();
                if (FilteringQuery.Any())
                {
                    foreach (var item in FilteringQuery)
                    {
                        LB.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Nem volt a keresésnek megfelelő elem, próbálja újra", "Keresési eredmény");
                }
            }
            else
            {
                foreach (var item in ListBoxItems)
                {
                    LB.Items.Add(item.ToString());
                }
            }
        }

        private void CB_Click(object sender, RoutedEventArgs e)
        {
            if(LB.SelectedItem!=null)
            {
                ((TodoItem)LB.SelectedItem).CompletedAt = CB.IsChecked == true
                    ? DateTime.Now
                    : null;
            }
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DelBTN.IsEnabled = LB.SelectedItem != null;
            if(LB.SelectedItem != null)
            {
                var selectedItem = (TodoItem)LB.SelectedItem;
                TBInput.Text = selectedItem.Value;
                DP.SelectedDate = selectedItem.Deadline;
                CB.IsChecked = selectedItem.CompletedAt != null;
            }
        }
    }
}