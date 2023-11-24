
//  ToDo app

using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace TodoApp
{
    public partial class MainWindow : Window
    {
        private List<TodoItem> TodoList;


        public MainWindow()
        {
            InitializeComponent();

            TodoList = new List<TodoItem>();
            lv_List.ItemsSource = TodoList;
            dp_Date.SelectedDate = DateTime.Today;
        }


        private void bt_ClearList_Click(object sender, RoutedEventArgs e)
        {
            //  Clear the list and reset filtering

            TodoList.Clear();
            tb_Search.Text = "";
            lv_List.Items.Filter = null;
            lv_List.Items.Refresh();
        }


        private void bt_Delete_Click(object sender, RoutedEventArgs e)
        {
            //  Remove selected items

            foreach (TodoItem item in lv_List.SelectedItems)    TodoList.Remove(item);
            lv_List.Items.Refresh();
        }


        private void bt_MarkDone_Click(object sender, RoutedEventArgs e)
        {
            //  Mark selected items as done, then mark them all as not done

            var query = from TodoItem item in lv_List.SelectedItems select item;
            var newState = query.All(item  =>  item.IsDone) ? false : true;

            foreach (var item in query)    item.IsDone = newState;

            TodoList.Sort();
            lv_List.Items.Refresh();
        }


        private void bt_NewTodo_Click(object sender, RoutedEventArgs e)
        {
            //  Create new ToDo item using selected data, or today's date if no date is selected yet

            var date = DateOnly.FromDateTime(dp_Date.SelectedDate ?? DateTime.Today);
            TodoList.Add(new TodoItem(tb_Description.Text, tb_Category.Text, date));

            lv_List.SelectedItem = TodoList.Last();
            TodoList.Sort();
            lv_List.Items.Refresh();
        }


        private void lv_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  If we select a single item, update the category, description, and date accordingly

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
            //  Split the search text into an array of words
            //  If any of the words appear in the category or description, we include the result

            var words = tb_Search.Text.Trim().ToLower().Split();

            //  Disable filtering if the search text is empty, or the ListView will stop functioning

            if (words[0] == "")    lv_List.Items.Filter = null;
            else                   lv_List.Items.Filter = item  =>  words.Any(((item as TodoItem)!.Description + " " + (item as TodoItem)!.Category).ToLower().Contains);

            lv_List.Items.Refresh();
        }

        private void bt_SaveList_Click(object sender, RoutedEventArgs e)
        {
            //  Do not bother opening the file dialog if the list is empty

            if (TodoList.Count == 0)    return;

            var dlg = new SaveFileDialog();
            dlg.Filter = "ToDo List|*.tod|All Files|*.*";
            dlg.Title = "Save ToDo List";
            dlg.ShowDialog();

            using (var stream = new StreamWriter(dlg.OpenFile()))
            {
                //  If the file stream was correctly opened, save the number of items in the list, followed by each of the items
                //  with all fields formatted as strings on separate lines

                stream.WriteLine(TodoList.Count);

                foreach (var item in TodoList)
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
                //  Read the number of items in the list file, and recreate the whole list one item at a time

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

                //  Replace with the newly read list, reset filtering, and refresh the ListView

                TodoList = newList;
                lv_List.ItemsSource = TodoList;
                lv_List.Items.Filter = null;
                tb_Search.Text = "";
                lv_List.Items.Refresh();
            }
        }
    }
}
