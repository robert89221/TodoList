
//  Todo-list application

using TodoApp;

const ConsoleColor YELLOW = ConsoleColor.Yellow;
const ConsoleColor GREEN = ConsoleColor.Green;
const ConsoleColor GRAY = ConsoleColor.Gray;
const ConsoleColor RED = ConsoleColor.Red;


var list = new TodoList();
PopulateList();

ShowList();




void Print(ConsoleColor c, object s)
{
    Console.ForegroundColor = c;
    Console.Write(s);
}


void PrintLine(ConsoleColor c, object s)
{
    Console.ForegroundColor = c;
    Console.WriteLine(s);
}


void ShowList()
{
    PrintLine(GRAY, "\nAtt göra:\n\nDatum       Kategori    Beskrivning\n");

    var today = DateOnly.FromDateTime(DateTime.Today);

    var query = from TodoItem item in list
                where item.IsDone == false
                orderby item.Date, item.Category
                select item;

    foreach (var result in query)
    {
        ConsoleColor col;

        if (result.Date == today) col = YELLOW;
        else if (result.Date < today) col = RED;
        else col = GRAY;

        PrintLine(col, result);
    }

    Console.WriteLine();

    query = from TodoItem item in list
            where item.IsDone == true
            orderby item.Date, item.Category
            select item;

    foreach (var result in query)    PrintLine(GREEN, result);
}




void PopulateList()
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
