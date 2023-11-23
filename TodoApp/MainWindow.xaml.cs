using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Security.Cryptography;
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

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //internal TodoList list;
        private List<TodoItem> list;

        public MainWindow()
        {
            InitializeComponent();

            list = new List<TodoItem>();

            lv_List.ItemsSource = list;

            dp_Date.SelectedDate = DateTime.Today;
        }


        private void bt_ClearList_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
            tb_Search.Text = "";
            lv_List.Items.Filter = null;

            lv_List.Items.Refresh();
        }


        private void bt_Delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (TodoItem item in lv_List.SelectedItems)    list.Remove(item);
            lv_List.Items.Refresh();
        }


        private void bt_MarkDone_Click(object sender, RoutedEventArgs e)
        {
            //  markera först alla som färdiga, sedan som ej färdiga

            var query = from TodoItem item in lv_List.SelectedItems select item;
            var newState = query.All(item => item.IsDone) ? false : true;

            foreach (var item in query)    item.IsDone = newState;

            list.Sort();
            lv_List.Items.Refresh();
        }


        private void bt_NewTodo_Click(object sender, RoutedEventArgs e)
        {
            var dt = DateOnly.FromDateTime(dp_Date.SelectedDate ?? DateTime.Today);
            list.Add(new TodoItem(tb_Description.Text, tb_Category.Text, dt));

            lv_List.SelectedItem = list.Last();
            list.Sort();
            lv_List.Items.Refresh();
        }


        private void lv_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_List.SelectedItems.Count == 1)
            {
                tb_Category.Text = (lv_List.SelectedItems[0] as TodoItem)!.Category;
                tb_Description.Text = (lv_List.SelectedItems[0] as TodoItem)!.Description;
                dp_Date.SelectedDate = (lv_List.SelectedItems[0] as TodoItem)!.Date.ToDateTime(TimeOnly.MinValue);

            } else {

                tb_Category.Text = tb_Description.Text = "";
            }
        }


        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            var words = tb_Search.Text.Trim().ToLower().Split();

            if (words[0] == "")
            {
                lv_List.Items.Filter = null;
            }
            else
            {
                lv_List.Items.Filter = item => words.Any(((item as TodoItem)!.Description + " " + (item as TodoItem)!.Category).ToLower().Contains);
            }

            lv_List.Items.Refresh();
        }

        private void bt_SaveList_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0)    return;

            var dlg = new SaveFileDialog();
            dlg.Filter = "ToDo List|*.tod|All Files|*.*";
            dlg.Title = "Save ToDo List";
            dlg.ShowDialog();

            using (var stream = new StreamWriter(dlg.OpenFile()))
            {
                stream.WriteLine(list.Count);

                foreach (var item in list)
                {
                    stream.WriteLine(item.IsDone);
                    stream.WriteLine(item.Date);
                    stream.WriteLine(item.Category);
                    stream.WriteLine(item.Description);
                }
            }                
        }


        private void bt_LoadList_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ToDo List|*.tod|All Files|*.*";
            dlg.Title = "Load ToDo List";
            dlg.Multiselect = false;
            dlg.CheckFileExists = true;
            dlg.ShowDialog();

            using (var stream = new StreamReader(dlg.OpenFile()))
            {
                int count = Convert.ToInt32(stream.ReadLine());
                var newList = new List<TodoItem>();

                for (int n = 0; n < count; ++n)
                {
                    var done = Convert.ToBoolean(stream.ReadLine());
                    var date = DateOnly.FromDateTime(Convert.ToDateTime(stream.ReadLine()));
                    var cat = stream.ReadLine();
                    var desc = stream.ReadLine();

                    newList.Add(new TodoItem(desc!, cat!, date, done));
                }

                list = newList;
                lv_List.ItemsSource = list;
                lv_List.Items.Filter = null;
                tb_Search.Text = "";
                lv_List.Items.Refresh();
            }
        }
    }
}
