using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexRestaurant.Repository;
using ApexRestaurant.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApexRestaurant.Api
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Startup(IConfiguration configuration)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        public IConfiguration Configuration { get; } = configuration;
        public object ServiceModule { get; private set; }

        // This method gets called by the runtime. Use this method to add services to

        public void ConfigureServices(IServiceCollection services)
{
RepositoryModule.Register(services,
Configuration.GetConnectionString("DefaultConnection"));
ServiceModule.Register(services);
services.AddControllers();
services.AddSwaggerGen();
services.AddMvc(option => option.EnableEndpointRouting = false);
services.AddCors(allowsites =>
{
allowsites.AddPolicy("AllowOrigin", options =>
options.AllowAnyOrigin());
});
}
// This method gets called by the runtime. Use this method to configure the

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
if (env.IsDevelopment())
{
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
}
else
{
app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
endpoints.MapDefaultControllerRoute();
});

app.UseStaticFiles();
app.UseMvc();
app.UseCors(options => options.AllowAnyOrigin());
}
}
}