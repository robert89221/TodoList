
//  Todo-list application

using System.Threading.Channels;
using TodoApp;

const ConsoleColor YELLOW = ConsoleColor.Yellow;
const ConsoleColor GREEN = ConsoleColor.Green;
const ConsoleColor GRAY = ConsoleColor.Gray;
const ConsoleColor RED = ConsoleColor.Red;


var list = new TodoList();
PopulateList();


while (true)
{
    Print(GRAY, "(N)  Ny todo\n" +
                "(V)  Visa listan\n" +
                "(R)  Radera listan\n" +
                "(M)  Modifiera todo\n" +
                "(S)  Sök i listan\n" +
                "(L)  Läs lista från disk\n" +
                "(A)  Spara aktuell lista och avsluta\n\n");

    while (true)
    {
        Print(GRAY, "Ditt val: "); 
        var val = Console.ReadLine().Trim().ToUpper();

        if (val == "N") { NewTodo(); break; }
        if (val == "V") { ShowList(); break; }
        if (val == "R") { EraseList(); break; }
        if (val == "M") { EditTodo(); break; }
        if (val == "S") { SearchTodo(); break; }
        if (val == "L") { LoadList(); break; }
        if (val == "A") { SaveList(); return 0; }
    }
}


void NewTodo()
{

}


void EraseList()
{
    list.EraseList();
    PrintLine(RED, "Listan har raderats");
}


void EditTodo() { }
void SearchTodo() { }
void LoadList() { }
void SaveList() { }


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


List<int> ShowList()
{
    var today = DateOnly.FromDateTime(DateTime.Today);
    var indices = new List<int>() { 0 };
    var taskNum = 1;

    if (list.Length == 0)
    {
        PrintLine(GRAY, "Listan är tom");
        return indices;
    }

    PrintLine(GRAY, "\n    Datum       Kategori    Beskrivning\n");

    var query = from TodoItem item in list
                orderby item.IsDone, item.Date, item.Category
                select item;

    foreach (var result in query)
    {
        ConsoleColor col;

        if (result.IsDone) col = GREEN;
        else if (result.Date == today) col = YELLOW;
        else if (result.Date < today) col = RED;
        else col = GRAY;

        PrintLine(col, $"{taskNum,2}  {result}");

        indices.Add(result.ID);
        ++taskNum;
    }

    Console.WriteLine();
    return indices;
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
