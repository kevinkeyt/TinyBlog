using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using TinyBlog.Core.Interfaces;
using TinyBlog.Infrastructure.Repos;
using TinyBlog.Infrastructure.DomainEvents;
using TinyBlog.Web.Extensions;
using TinyBlog.Web.Interfaces;
using TinyBlog.Web.Services;
using Westwind.AspNetCore.Markdown;
using TinyBlog.Core.Entities;
using TinyBlog.Core.SharedKernel;

namespace TinyBlog.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Infrastructure Services

            // Uncomment for Azure Table Repository
            // services.AddScoped<IAzureTableStorage<Post>>(factory =>
            // {
            //     return new AzureTableStorage<Post>(
            //         new AzureTableSettings(
            //             storageAccount: Configuration["StorageAccount"],
            //             storageKey: Configuration["StorageKey"],
            //             tableName: "Posts"));
            // });
            // services.AddScoped<IPostRepository, AzureTablePostRepository>();
            // services.AddScoped<IAzureTableStorage<Blog>>(factory =>
            // {
            //     return new AzureTableStorage<Blog>(
            //         new AzureTableSettings(
            //             storageAccount: Configuration["StorageAccount"],
            //             storageKey: Configuration["StorageKey"],
            //             tableName: "BlogInfo"));
            // });
            // services.AddScoped<IBlogRepository, AzureTableBlogRepository>();

            // Uncomment for File Repository
            services.AddScoped<IBlogRepository, BlogFileRepository>();
            services.AddScoped<IPostRepository, PostRepository>();


            // WebSite Services
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IPostService, PostService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Admin");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));

            services.AddMarkdown(config =>
            {
                // Create custom MarkdigPipeline 
                // using MarkDig; for extension methods
                config.ConfigureMarkdigPipeline = b =>
                {
                    b.UseEmphasisExtras(Markdig.Extensions.EmphasisExtras.EmphasisExtraOptions.Default)
                        .UsePipeTables()
                        .UseGridTables()
                        .UseAutoIdentifiers(AutoIdentifierOptions.GitHub) // Headers get id="name" 
                        .UseAutoLinks() // URLs are parsed into anchors
                        .UseAbbreviations()
                        .UseYamlFrontMatter()
                        .UseEmojiAndSmiley(true)
                        .UseListExtras()
                        .UseFigures()
                        .UseTaskLists()
                        .UseCustomContainers()
                        .UseGenericAttributes();
                };
            });

            // Configure Autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);

            // Configure Domain Events
            builder.RegisterType<DomainEventDispatcher>().As<IDomainEventDispatcher>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IHandle<>))))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
