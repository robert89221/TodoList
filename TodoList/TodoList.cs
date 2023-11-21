using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TodoApp
{
    internal class TodoList:IEnumerable
    {
        private List<TodoItem> Items { get; set; }
        private int idCount { get; set; }
        public int Length { get  =>  (int)Items.LongCount(); }

        public TodoList()
        {
            idCount = 0;
            Items = new List<TodoItem>();
        }

        public void RemoveTodo(int n)  =>  Items.RemoveAt(n);

        public void EraseList()  =>  Items = new List<TodoItem>();

        public void AddTodo(TodoItem t, bool d = false)
        {
            t.IsDone = d;
            t.ID = idCount;
            Items.Add(t);
            ++idCount;
        }

        public IEnumerator GetEnumerator()  =>  Items.GetEnumerator();
    }
}
