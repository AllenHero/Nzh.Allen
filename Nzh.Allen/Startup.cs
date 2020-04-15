using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nzh.Allen.Repository;
using Module = Autofac.Module;

namespace Nzh.Allen
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //不加这句会（视图中的中文被html编码）
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //内存缓存
            services.AddMemoryCache();

            services.AddDistributedMemoryCache();

            //配置session(session是根据上面cache来区分存储源地的)
            services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(60); //设置Session闲置超时时间(有效时间周期)
                opts.Cookie.Name = "Nzh.Allen_cookie";
                opts.Cookie.HttpOnly = true;

            });


            //配置存储视图的默认区域文件夹
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Views/{2}/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews()
               //配置json返回的格式
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.PropertyNamingPolicy = null;
                   options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                   options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
               })
                // 将Controller加入到Services中
                .AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Login/Error");
            }
            else
            {
                app.UseExceptionHandler("/Login/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            //配置模块目录
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                     "Permissions_route", "Permissions", "Permissions/{controller=Index}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                 "Basic_route", "Basic", "Basic/{controller=Index}/{action=Index}/{id?}");

            });
        }
        #region autofac

        /// <summary>
        /// 这里添加的方法来用autofac注入服务
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RmesAutoFacModule());
        }

        public class RmesAutoFacModule : Module
        {
            private static Autofac.IContainer _container;

            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<MySqlHelper>()
                    .PropertiesAutowired()
                    .InstancePerDependency();

                //WebAPI只用引用services和repository的接口，不用引用实现的dll。
                //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
                var iServices = Assembly.Load("Nzh.Allen.IService");
                var services = Assembly.Load("Nzh.Allen.Service");
                var iRepository = Assembly.Load("Nzh.Allen.IRepository");
                var repository = Assembly.Load("Nzh.Allen.Repository");

                //根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
                builder.RegisterAssemblyTypes(iServices, services)
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces().PropertiesAutowired()//允许属性注入
                  .InstancePerDependency();//默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；

                //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
                builder.RegisterAssemblyTypes(iRepository, repository)
                  .Where(t => t.Name.EndsWith("Repository"))
                  .AsImplementedInterfaces().PropertiesAutowired()
                  .InstancePerDependency();


                var controllerBaseType = typeof(ControllerBase);
                builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                    .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                    .PropertiesAutowired() // 允许属性注入
                    .EnableClassInterceptors(); // 允许在Controller类上使用拦截器
                // 手动高亮
                //builder.RegisterBuildCallback(container => _container = container);
            }

            public static Autofac.IContainer GetContainer()
            {
                return _container;
            }
        }

        #endregion

        public class DateTimeConverter : JsonConverter<DateTime>
        {
            /// <summary>
            /// 获取或设置DateTime格式
            /// <para>默认为: yyyy-MM-dd HH:mm:ss</para>
            /// </summary>
            public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                => DateTime.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString(this.DateTimeFormat));
        }
    }
}
