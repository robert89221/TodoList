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
        internal TodoList list;

        public MainWindow()
        {
            InitializeComponent();

            list = new TodoList();
            PopulateList();
            this.lv_List.ItemsSource = list;
        }

        private void PopulateList()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

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
        }

    }
}
