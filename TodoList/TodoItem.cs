using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    internal class TodoItem
    {
        public string Text { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public bool IsDone { get; set; }

        public TodoItem(string t, string c, DateOnly d)  =>  (Text, Category, Date, IsDone) = (t, c, d, false);

        override public string ToString()  =>  $"{Date,12}{Category,10}{Text,30}";
    }
}
