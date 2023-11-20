
//  Todo-list application

using TodoApp;

var list = new TodoList();

list.AddTodo(new TodoItem("fixa mat", "mat", new DateOnly(2000, 3, 4)));
list.AddTodo(new TodoItem("städa", "hem", new DateOnly(2001, 5, 6)));

foreach(var t in list)     Console.WriteLine(t);
