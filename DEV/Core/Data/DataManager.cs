using Core.Common;
using Core.Manager;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Core.Data
{
    public class DataManager: Singleton<DataManager>,IManager
    {
        #region ServiceCollection
        public void ServiceCollection(IServiceCollection services)
        {
            RegisterEFService(services);
            RegisterRespositoryServices(services);
        }

        /// <summary>
        /// 注册EF服务
        /// </summary>
        /// <param name="services"></param>
        public void RegisterEFService(IServiceCollection services)
        {
            var mySqlConnection = "Data Source=localhost;port=3306;Initial Catalog=TestData;uid=root;password=123456;Charset=utf8;SslMode=None;";

            //增加EF服务
            services.AddDbContext<MySqlDbContext>(options => options.UseMySql(mySqlConnection));
        }

        /// <summary>
        /// 注册数据仓储
        /// 第三方Respository:https://www.cnblogs.com/xiaoliangge/p/7231715.html
        /// </summary>
        /// <param name="services"></param>
        public void RegisterRespositoryServices(IServiceCollection services)
        {
            services.AddUnitOfWork<MySqlDbContext>();
        }
        #endregion

        #region ApplicationBuilder
        public void ApplicationBuilder(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context)
        {
            InitializeDB(context);
        }

        /// <summary>
        /// 检查数据库是否存在
        /// </summary>
        public void InitializeDB(IServiceProvider context)
        {
            //var migrations = await context.Database.GetPendingMigrationsAsync();//获取未应用的Migrations，不必要，MigrateAsync方法会自动处理
            //根据migrations修改/创建数据库
            using (var ser = context.CreateScope())
            {
                ser.ServiceProvider.GetService<MySqlDbContext>().Database.Migrate();
            }
        }
        #endregion
    }
}
