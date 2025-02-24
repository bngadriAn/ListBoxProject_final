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
        private List<TodoItem> toDoItems = new List<TodoItem>();
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
                toDoItems = File.ReadAllLines(FilePath).Select(x=>TodoItem.FromCsv(x)).ToList();
                toDoItems.ForEach(x => LB.Items.Add(x));
            }
        }
        
        private void EditBTN_Click(object sender, RoutedEventArgs e)
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
                string searchQuery = TBSearch.Text.Trim().ToLower();
                
                var q2 = toDoItems.Select(x=>(item: x, label: x.Value!.Trim().ToLower()))
                            .Where(x=>x.label.Contains(searchQuery))
                            .OrderByDescending(x => x.label.StartsWith(searchQuery))
                            .ThenBy(x => x.label)
                            .Select(x => x.item);

                //LB.Items.Refresh();
                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    LB.Items.Clear();
                    toDoItems.ForEach(x => LB.Items.Add(x));
                }
                else
                {
                    IEnumerable<TodoItem> q = from x in toDoItems
                            let label = x.Value!.Trim().ToLower()
                            where label.Contains(searchQuery)
                            orderby label.StartsWith(searchQuery) descending
                            orderby label ascending
                            select x;
                    foreach (var item in q)
                    {
                        LB.Items.Add(item);
                    }
                }
            }
            else
            {
                LB.Items.Clear();
                foreach (var item in toDoItems)
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

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2();
            win2.Show();
        }
    }
}