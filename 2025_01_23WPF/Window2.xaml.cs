using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private const string FilePath = "todo.csv";
        private List<TodoItem> toDoItems = new List<TodoItem>();
        public Window2()
        {
            InitializeComponent();
        }

        private void SaveItem()
        {
            var operation = new List<string>
            {
                //TBInput.Text.ToCsv()
            };
            File.WriteAllLines(FilePath, operation);
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            if(TBInput.Text != null)
            {
                var operation = new List<string>();
                var item = new TodoItem
                {
                    Value = TBInput.Text,
                    Deadline = DeadLineDP.SelectedDate,
                    CompletedAt = CB.IsChecked == true ? DateTime.Now : null
                };
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

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
