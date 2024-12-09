using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevTest_CostAccounting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CADbContext>(options => options.UseInMemoryDatabase(databaseName: "DbInMemory"));
            builder.Services.AddTransient(typeof(IClientRepository), typeof(ClientRepository));
            builder.Services.AddTransient(typeof(ICompanyRepository), typeof(CompanyRepository));
            builder.Services.AddTransient(typeof(IInvestmentRepository), typeof(InvestmentRepository));
            builder.Services.AddTransient(typeof(ITransactionRepository), typeof(TransactionRepository));
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IInvestmentService, InvestmentService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var mScope = app.Services.CreateScope();
            var context = mScope.ServiceProvider.GetRequiredService<CADbContext>();
            AddTestData(context);

            app.Run();
        }

        private static void AddTestData(CADbContext context)
        {
            context.Clients.AddRange(
                new Client { Id = 1, Name = "John Smith", Email="jsmith@test.com" },
                new Client { Id = 2, Name = "Peter Pan", Email="ppan@test.com" }
            );
            context.Companies.AddRange(
                new Company { Id = 1, Name = "Microsoft Corp", SharePrice = decimal.Parse("20.00") },
                new Company { Id = 2, Name = "NVIDIA Corp", SharePrice = decimal.Parse("142.44") },
                new Company { Id = 3, Name = "PepsiCo Inc", SharePrice = decimal.Parse("157.79") }
            );
            context.TransactionTypes.AddRange(
                new TransactionType { Id = 1, Description = "Buy" },
                new TransactionType { Id = 2, Description = "Sell" }
            );
            context.Investments.AddRange(
                new Investment { ClientId = 1, CompanyId = 1, Date = DateTime.Parse("01/01/2024"), Shares = 100, Cost = Decimal.Parse("20.00") },
                new Investment { ClientId = 1, CompanyId = 1, Date = DateTime.Parse("02/01/2024"), Shares = 150, Cost = Decimal.Parse("30.00") },
                new Investment { ClientId = 1, CompanyId = 1, Date = DateTime.Parse("03/01/2024"), Shares = 120, Cost = Decimal.Parse("10.00") }
            );
            context.Transactions.AddRange(
                new Transaction { ClientId = 1, CompanyId = 1, Date = DateTime.Parse("01/01/2024"), TypeId = 1, Shares = 100, Rate = Decimal.Parse("20.00") },
                new Transaction { ClientId = 1, CompanyId = 1, Date = DateTime.Parse("02/01/2024"), TypeId = 1, Shares = 150, Rate = Decimal.Parse("30.00") },
                new Transaction { ClientId = 1, CompanyId = 1, Date = DateTime.Parse("03/01/2024"), TypeId = 1, Shares = 120, Rate = Decimal.Parse("10.00") }
            );
            context.SaveChanges();
        }
    }
}
