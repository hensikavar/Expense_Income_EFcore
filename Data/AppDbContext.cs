using Microsoft.EntityFrameworkCore;
using RKIT.Models;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<IncomeModel> Incomes { get; set; }
    public DbSet<ExpenseModel> Expenses { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
}