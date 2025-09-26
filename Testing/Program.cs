using Application.Common;
using Application.DTOs.ToDoItemDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = stepsdb; Trusted_Connection = True");

var options = optionsBuilder.Options;

using (var ctx = new ToDoListDBContext(options))
{
    var todoRepo = new ToDoItemRepository(ctx);
    var userRepo = new UserRepository(ctx);
    var todoServ = new ToDoItemService(todoRepo);
    var userServ = new UserService(userRepo);
    // test infrastructure
    /*
    foreach (var user in ctx.Users)
    {
        Console.WriteLine($"Id: {user.Id} , username: {user.UserName}");
    }

    foreach (var item in ctx.ToDoItems)
    {
        Console.WriteLine($"Id: {item.Id} , title: {item.Title}");
    }
    */

    //test ItemsRepository

    //IToDoItemRepository repo = new ToDoItemRepository(ctx);
    //var items = repo.GetAllAsync(new Application.Common.ToDoItemRequestParameters()).Result.Items;
    //foreach (var item in items)
    //{
    //    Console.WriteLine($"Id: {item.Id} , title: {item.Title}");
    //}

    //test ItemsService

    IToDoItemService serv = todoServ;
    var dto = new UpdateToDoItemDto()
    {
        Id = 12,
        Title = "edited task",
        IsCompleted = true
    };

    await serv.UpdateAsync(1, dto);
    var parameters = new ToDoItemRequestParameters()
    {
        PageSize = 50
    };

    var items = serv.GetAllAsync(parameters).Result.Items;
    foreach (var item in items)
    {
        Console.WriteLine($"Id: {item.Id} , title: {item.Title} , completed: {item.IsCompleted}");
    }
}