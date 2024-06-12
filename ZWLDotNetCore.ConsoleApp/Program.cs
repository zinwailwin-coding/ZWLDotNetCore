using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using ZWLDotNetCore.ConsoleApp.AdoDotNetExamples;
using ZWLDotNetCore.ConsoleApp.DapperExamples;
using ZWLDotNetCore.ConsoleApp.EFCoreExamples;
using ZWLDotNetCore.ConsoleApp.Services;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("Biography", "Dr. Ye Thu Wai", "This is the history book");
//adoDotNetExample.Update(1003, "Test Title","Test Author", "TEst Content");
//adoDotNetExample.Delete(1003);
//adoDotNetExample.Edit(1002);
//adoDotNetExample.Edit(1002);

//DapperExample dapper= new DapperExample();
//dapper.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();
//Console.ReadKey();

var connectionString= ConnectionStrings.SqlConnectionStringBuilder.ConnectionString;
var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
var serviceProvider = new ServiceCollection()
    .AddScoped(n=>new AdoDotNetExample(connectionStringBuilder))
    .AddScoped(n=>new DapperExample(connectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();
var efCore = serviceProvider.GetRequiredService<AdoDotNetExample>();
efCore.Read();

//var adoDotNet= serviceProvider.GetRequiredService<AdoDotNetExample>();
//adoDotNet.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

