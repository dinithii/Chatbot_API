using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args); //new web application
builder.WebHost.UseUrls("http://localhost:5000"); //server to run on
var app = builder.Build(); //builds the app

// In memory chatbot responses
Dictionary<string, string> chatbotMemory = new()
{
    { "hello", "Hi there! How can I assist you?" },
    { "how are you", "I'm just a bot, but I'm doing great! Thanks for asking." },
    { "your name", "I'm a simple chatbot built in C#." },
    { "bye", "Goodbye! Have a great day!" }
};

// Default route
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Yayyy chatbot API is runningggg!!!! Use /chat?message=your_text to interact.");
});

// Chatbot API route
app.MapGet("/chat", async (HttpContext context) =>
{
    var query = context.Request.Query["message"].ToString().ToLower();

    if (chatbotMemory.ContainsKey(query))
    {
        await context.Response.WriteAsync(chatbotMemory[query]);
    }
    else
    {
        await context.Response.WriteAsync("Sorry, I don't understand that.");
    }
});

app.Run();
