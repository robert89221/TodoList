
namespace TodoApp
{
    //  A class representing a todo-item

    internal class TodoItem:IComparable<TodoItem>
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public string DateString
        {
            //  The DateString property is used to enhance the date formatting when shown in the ListView

            get
            {
                if (IsDone)                                                return "Done";
                else if (Date == DateOnly.FromDateTime(DateTime.Today))    return "Today";
                else                                                       return Date.ToString();
            }
        }
        public bool IsDone { get; set; }

        public TodoItem(string d, string c, DateOnly date, bool done = false)  =>  (Description, Category, Date, IsDone) = (d, c, date, done);
        
        public int CompareTo(TodoItem? that)
        {
            //  Implement IComparable to control the sorting order in the ListView

            var a = $"{this.IsDone}{this.Date}{this.Category}";
            var b = $"{that!.IsDone}{that!.Date}{that!.Category}";
            return a.CompareTo(b);
        }
        
    }
}
