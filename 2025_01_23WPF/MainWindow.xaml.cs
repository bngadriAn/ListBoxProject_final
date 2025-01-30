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

namespace _2025_01_23WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*public class searchedItem
        {
            private string Searched;
            public searchedItem(string item)
            {
                this.Searched = item;
            }
        }*/
        public List<string> ListBoxItems { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ListBoxItems = [];
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TBInput.Text != "")
            {
                ListBoxItems.Add(TBInput.Text);
                LB.Items.Add(TBInput.Text);
            }
        }
        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            int LBindex = LB.SelectedIndex;
            LB.Items.RemoveAt(LBindex);
            LB.Items.Insert(LBindex, TBInput.Text);
            TBInput.Text = null;
        }

        private void DelBTN_Click(object sender, RoutedEventArgs e)
        {
            LB.Items.RemoveAt(LB.Items.IndexOf(LB.SelectedItem));
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
                    from item in ListBoxItems
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
    }
}