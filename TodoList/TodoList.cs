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
        private List<TodoItem> Items { get; }
        private int idCount { get; set; }

        public TodoList()
        {
            idCount = 0;
            Items = new List<TodoItem>();
        }

        public void RemoveTodo(int n)  =>  Items.RemoveAt(n);

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
