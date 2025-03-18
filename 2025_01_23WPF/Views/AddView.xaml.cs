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
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace _2025_01_23WPF.Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : Page
    {
        private const string FilePath = "todo.csv";
        private List<TodoItem> toDoItems = new List<TodoItem>();
        public AddView()
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
                toDoItems = File.ReadAllLines(FilePath).Select(x => TodoItem.FromCsv(x)).ToList();
                toDoItems.ForEach(x => LB.Items.Add(x));
            }
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            if(TBInput.Text != "")
            {
                if (LB.SelectedItem != null)
                {
                    var selectedItem = (TodoItem)LB.SelectedItem;
                    selectedItem.Deadline = DeadLineDP.SelectedDate;
                    selectedItem.Value = TBInput.Text;
                }
                else
                {
                    var item = new TodoItem
                    {
                        Value = TBInput.Text,
                        Deadline = DeadLineDP.SelectedDate,
                        CompletedAt = CB.IsChecked == true ? DateTime.Now : null
                    };
                    LB.Items.Add(item);
                }
                TBInput.Clear();
                CB.IsChecked = false;
                DeadLineDP.SelectedDate = null;
                SaveItem();
            }
            else
            {
                MessageBox.Show("Please enter a value", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB.SelectedItem != null)
            {
                var selectedItem = (TodoItem)LB.SelectedItem;
                TBInput.Text = selectedItem.Value;
                DeadLineDP.SelectedDate = selectedItem.Deadline;
                CB.IsChecked = selectedItem.CompletedAt != null;
            }
        }

        private void CB_Click(object sender, RoutedEventArgs e)
        {
            if (LB.SelectedItem != null)
            {
                ((TodoItem)LB.SelectedItem).CompletedAt = CB.IsChecked == true
                    ? DateTime.Now
                    : null;
            }
        }
    }
}
