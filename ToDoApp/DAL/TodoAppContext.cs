using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.DAL
{
    public class TodoAppContext:DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options):base(options){}
        public DbSet<TodoItem> todoItems { get; set; }
    }
}
