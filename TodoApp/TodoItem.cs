using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    internal class TodoItem : IComparable<TodoItem>
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public string DateString
        {
            get
            {
                if (IsDone) return "Done";
                else if (Date == DateOnly.FromDateTime(DateTime.Today)) return "Today";
                else return Date.ToString();
            }
        }
        public bool IsDone { get; set; }

        public TodoItem(string d, string c, DateOnly date, bool done = false) => (Description, Category, Date, IsDone) = (d, c, date, done);
        
        public int CompareTo(TodoItem? that)
        {
            var a = $"{this.IsDone}{this.Date}{this.Category}";
            var b = $"{that!.IsDone}{that!.Date}{that!.Category}";
            return a.CompareTo(b);
        }
        
    }
}
