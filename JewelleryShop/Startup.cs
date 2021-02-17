using Autofac;
using Autofac.Extensions.DependencyInjection;
using JewelleryShop.Interfaces;
using JewelleryShop.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryShop
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			ContainerBuilder builder = Builder();

			builder.Populate(services);
			var container = builder.Build();
			return new AutofacServiceProvider(container);
			//	services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			var context = serviceProvider.GetService<ApiContext>();
			AddTestData(context);

			app.UseHttpsRedirection();
			app.UseMvc();
		}

		private static void AddTestData(ApiContext context)
		{
			var testUser1 = new DbModels.Customer
			{
				Id = 1,
				FirstName = "Vivek",
				LastName = "Anand",
				Type = "Privileged",
				Email = "vivek@gmail.com",
				Password = "vivek_anand",
				DiscountPercentage = 0.02
			};

			context.Customers.Add(testUser1);

			var testUser2 = new DbModels.Customer
			{
				Id = 2,
				FirstName = "Ritu Parno",
				LastName = "Behera",
				Type = "Normal",
				Email = "ritu@gmail.com",
				Password = "ritu_parno",
				DiscountPercentage = 0.0
			};
			context.Customers.Add(testUser2);

			context.SaveChanges();
		}

		private static ContainerBuilder Builder()
		{
			var builder = new ContainerBuilder();
			//Add Repository Dependencies
			builder.RegisterType<ShoppingRepository>().As<IShoppingRepository>();

			return builder;
		}
	}
}
