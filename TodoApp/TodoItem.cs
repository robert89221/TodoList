using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    internal class TodoItem
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
        public int ID { get; set; }

        public TodoItem(string d, string c, DateOnly date, bool done = false)  =>  (Description, Category, Date, IsDone) = (d, c, date, done);

        override public string ToString()
        {
            string dateString;

            var today = DateOnly.FromDateTime(DateTime.Today);
            var tomorrow = today.AddDays(1);

            if (IsDone)                   dateString = "Klar";
            else if (Date == today)       dateString = "Idag";
            else if (Date == tomorrow)    dateString = "Imorgon";
            else                          dateString = Date.ToString();

            return $"{dateString,-10}  {Category,-10}  {Description,-50}";
        }
    }
}
