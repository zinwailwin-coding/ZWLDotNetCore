using System.Data;
using System.Data.SqlClient;
using ZWLDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("Biography", "Dr. Ye Thu Wai", "This is the history book");
//adoDotNetExample.Update(1003, "Test Title","Test Author", "TEst Content");
//adoDotNetExample.Delete(1003);
//adoDotNetExample.Edit(1002);
//adoDotNetExample.Edit(1002);
DapperExample dapper= new DapperExample();
dapper.Run();
Console.ReadKey();
