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

        public TodoList()  =>  Items = new List<TodoItem>();

        public void RemoveTodo(int n)  =>  Items.RemoveAt(n);

        public void AddTodo(TodoItem t)  =>  Items.Add(t);

        public IEnumerator GetEnumerator()  =>  Items.GetEnumerator();
    }
}
