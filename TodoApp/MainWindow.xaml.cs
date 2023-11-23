﻿using System.Text;
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

            //list = new TodoList();
            list = new List<TodoItem>();
            PopulateList();
            lv_List.ItemsSource = list;

            dp_Date.SelectedDate = DateTime.Today;
        }


        private void PopulateList()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            /*
                        list.AddTodo(new TodoItem("fixa mat", "Mat", today.AddDays(0)));
                        list.AddTodo(new TodoItem("städa", "Hem", today.AddDays(1)));
                        list.AddTodo(new TodoItem("ring bengt", "Kompisar", today.AddDays(1)));
                        list.AddTodo(new TodoItem("ring olle angående bilen", "Kompisar", today.AddDays(1)));
                        list.AddTodo(new TodoItem("köp ny bil", "Bil", today.AddDays(3)));
                        list.AddTodo(new TodoItem("köp ny cykel", "Cykel", today.AddDays(10)));
                        list.AddTodo(new TodoItem("förnya pendlingskortet", "Jobb", today.AddDays(21)));
                        list.AddTodo(new TodoItem("mickes födelsedag", "Kompisar", today.AddDays(85)));

                        list.AddTodo(new TodoItem("köp kattmat", "Husdjur", today.AddDays(-2)));

                        list.AddTodo(new TodoItem("klappa katten", "Husdjur", today.AddDays(-1)), true);
                        list.AddTodo(new TodoItem("gå ut med hunden", "Husdjur", today.AddDays(0)), true);
              */
            list.Add(new TodoItem("fixa mat", "Mat", today.AddDays(0)));
            list.Add(new TodoItem("städa", "Hem", today.AddDays(1)));
            list.Add(new TodoItem("ring bengt", "Kompisar", today.AddDays(1)));
            list.Add(new TodoItem("ring olle angående bilen", "Kompisar", today.AddDays(1)));
            list.Add(new TodoItem("köp ny bil", "Bil", today.AddDays(3)));
            list.Add(new TodoItem("köp ny cykel", "Cykel", today.AddDays(10)));
            list.Add(new TodoItem("förnya pendlingskortet", "Jobb", today.AddDays(21)));
            list.Add(new TodoItem("mickes födelsedag", "Kompisar", today.AddDays(85)));

            list.Add(new TodoItem("köp kattmat", "Husdjur", today.AddDays(-2)));

            list.Add(new TodoItem("klappa katten", "Husdjur", today.AddDays(-1), true));
            list.Add(new TodoItem("gå ut med hunden", "Husdjur", today.AddDays(0), true));
        }


        private void bt_ClearList_Click(object sender, RoutedEventArgs e)
        {
            list.Clear();
            lv_List.Items.Refresh();
        }


        private void bt_Delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (TodoItem item in lv_List.SelectedItems)    list.Remove(item);
            lv_List.Items.Refresh();
        }


        private void bt_MarkDone_Click(object sender, RoutedEventArgs e)
        {
            //  markera först alla som klara, sedan som oklara

            var query = from TodoItem item in lv_List.SelectedItems select item;
            var newState = query.All(item => item.IsDone) ? false : true;

            foreach (var item in query)    item.IsDone = newState;

            lv_List.Items.Refresh();
        }


        private void bt_NewTodo_Click(object sender, RoutedEventArgs e)
        {
            var dt = DateOnly.FromDateTime(dp_Date.SelectedDate ?? DateTime.Today);
            list.Add(new TodoItem(tb_Description.Text, tb_Category.Text, dt));
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
    }
}
