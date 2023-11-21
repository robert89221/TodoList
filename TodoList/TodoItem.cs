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
        public int ID { get; set; }

        public TodoItem(string t, string c, DateOnly d)  =>  (Text, Category, Date, IsDone) = (t, c, d, false);

        override public string ToString()
        {
            string dateString;

            var today = DateOnly.FromDateTime(DateTime.Today);
            var tomorrow = today.AddDays(1);

            if (IsDone)                   dateString = "Färdigt";
            else if (Date == today)       dateString = "Idag";
            else if (Date == tomorrow)    dateString = "Imorgon";
            else                          dateString = Date.ToString();

            return $"{dateString,-10}  {Category,-10}  {Text,-50}";
        }
    }
}
