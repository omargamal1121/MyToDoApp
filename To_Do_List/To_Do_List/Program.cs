using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Areas.UserArea.Data;
using To_Do_List.Areas.UserArea.Repository;
using To_Do_List.Models;
using Umbraco.Core.Services.Implement;
using EmailModel = To_Do_List.Models.EmailModel;
namespace To_Do_List
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Connection") ?? throw new InvalidOperationException("Connection string 'ContextConnection' not found.");;
			builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
			builder.Services.AddTransient<IEmailSender, EmailModel>();
			builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Context>();

            // Add services to the container.
            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            builder.Services.AddScoped(typeof(IMainCategoryRepository),typeof(MainCategoryRepository));
            builder.Services.AddScoped(typeof(ISubCategoryRepository),typeof(SubCategoryRepository));
            builder.Services.AddScoped(typeof(ITasksRepository),typeof(TasksRepository));
            builder.Services.AddControllersWithViews();
		

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
			app.UseEndpoints(x => x.MapRazorPages());
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "MainRoute",
                pattern: "{Area:exists}/{controller=User}/{action=Index}/{id?}",
                defaults: new { Area = "UserArea" })
                .WithStaticAssets();

            app.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}")
                         .WithStaticAssets();

            app.Run();
        }
    }
}
