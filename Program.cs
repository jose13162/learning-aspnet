using asp_net_core.Data;
using asp_net_core.Models;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();

// configure swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// using swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/todos", (DataContext context) => {
	context.Todos.Add(new Todo() {
		Id = Guid.NewGuid(),
		Title = "Ir para a academia",
		Done = true
	});

	context.SaveChanges();

	var todos = context.Todos.ToList();

	return Results.Ok(todos);
})
	.Produces<Todo>(200)
	.Produces(500);

app.Run();
