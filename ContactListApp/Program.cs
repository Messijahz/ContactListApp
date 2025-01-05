using Microsoft.Extensions.DependencyInjection;
using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Data;
using ContactListApp.Business.Services;
using Presentation.Console.MainApp.Services;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IFileService, FileService>()
    .AddSingleton<IContactService, ContactService>()
    .AddSingleton<IMenuService, MenuService>()
    .BuildServiceProvider();

var menuService = serviceProvider.GetService<IMenuService>();


menuService?.Show();