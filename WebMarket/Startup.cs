using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebMarket.Pipeline.CartItemLogic;
using WebMarket.Pipeline.CategoryLogic;
using WebMarket.Pipeline.ItemLogic;
using AutoMapper;
using WebMarket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using System.IO;
using static Microsoft.AspNetCore.Mvc.CompatibilityVersion;
using WebMarket.Model.MappingProfile;

namespace WebMarket
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = true).SetCompatibilityVersion(Latest);
            services.AddTransient<IAddCartItemPipeline, AddCartItemPipeline>();
            services.AddTransient<IDeleteCartItemPipeline, DeleteCartItemPipeline>();
            services.AddTransient<IUpdateCartItemPipeline, UpdateCartItemPipeline>();
            services.AddTransient<IGetCartItemPipeline, GetCartItemPipeline>();
            services.AddTransient<IGetAllCartItemPipeline, GetAllCartItemPipeline>();
            services.AddTransient<IAddCategoryPipeline, AddCategoryPipeline>();
            services.AddTransient<IDeleteCategoryPipeline, DeleteCategoryPipeline>();
            services.AddTransient<IUpdateCategoryPipeline, UpdateCategoryPipeline>();
            services.AddTransient<IGetCategoryPipeline, GetCategoryPipeline>();
            services.AddTransient<IGetAllCategoryPipeline, GetAllCategoryPipeline>();
            services.AddTransient<IAddItemPipeline, AddItemPipeline>();
            services.AddTransient<IDeleteItemPipeline, DeleteItemPipeline>();
            services.AddTransient<IUpdateItemPipeline, UpdateItemPipeline>();
            services.AddTransient<IGetItemPipeline, GetItemPipeline>();
            services.AddTransient<IGetAllItemPipeline, GetAllItemPipeline>();
            var config = new MapperConfiguration(c => {
                c.AddProfile<CartItemProfile>();
                c.AddProfile<CategoryProfile>();
                c.AddProfile<ItemProfile>();
            });
            services.AddSingleton<IMapper>(s => config.CreateMapper());
            services.AddAutoMapper();
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });
            services.AddOptions();
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    //options.IncludeXmlComments(XmlCommentsFilePath);
                });
            services.AddCors(options => options.AddPolicy(MyAllowSpecificOrigins,
                corsBuilder =>
                {
                    corsBuilder.AllowAnyMethod()
                        .WithOrigins("http://localhost:8080").AllowAnyHeader();
                }));
            var sqlConnectionString = Configuration["Db:ConnectionString"];
            services.AddDbContext<MarketContext>(options =>
                options.UseSqlServer(sqlConnectionString, b => b.MigrationsAssembly("WebMarket.Data"))
                //options.UseNpgsql(sqlConnectionString, b => b.MigrationsAssembly("Data"))
                //options.UseSqlite(sqlConnectionString, b => b.MigrationsAssembly("Data"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
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

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
