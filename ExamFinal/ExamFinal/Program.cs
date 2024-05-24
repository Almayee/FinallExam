using FinalExam.Business.Services.Abstracts;
using FinalExam.Business.Services.Concretes;
using FinalExam.Core.Models;
using FinalExam.Core.RepositoryAabstracts;
using FinalExam.Data.DAL;
using FinalExam.Data.RepositoryConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExamFinal
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AppDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});
			
			builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
				opt.Password.RequireNonAlphanumeric=true;
				opt.Password.RequireUppercase=true;
				opt.Password.RequireLowercase=true;
				opt.Password.RequireDigit=true;
				opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			builder.Services.AddScoped<IProductRepository ,ProductRepository>();
			builder.Services.AddScoped<IProductService,ProductService>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

			app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
		  );

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
