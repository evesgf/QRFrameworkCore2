using Core.Common;
using Core.Manager;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.AspNetCore.TimedJob;
using System;

namespace Core.TimeJob
{
    /// <summary>
    /// TimedJob使用：http://www.1234.sh/post/pomelo-extensions-timed-jobs
    /// Begin 起始时间；Interval执行时间间隔，单位是毫秒，建议使用以下格式，此处为3小时；SkipWhileExecuting是否等待上一个执行完成，true为等待；
    /// </summary>
    public class TimeJobManager: Singleton<TimeJobManager>,IManager
    {
        public TimedJobService timedJobService;

        #region ServiceCollection
        public void ServiceCollection(IServiceCollection services)
        {
            //添加TimeJob
            services.AddTimedJob().AddEntityFrameworkDynamicTimedJob<MySqlDbContext>();

            RegisterTimedJobServices(services);
        }

        /// <summary>
        /// 注册TimeJob任务
        /// </summary>
        /// <param name="services"></param>
        public void RegisterTimedJobServices(IServiceCollection services)
        {
            services.AddTransient<ITimeJobService, TimeJobService>();
        }
        #endregion

        #region ApplicationBuilder
        public void ApplicationBuilder(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider context)
        {
            //使用TimeJob
            app.UseTimedJob();

            timedJobService = app.ApplicationServices.GetRequiredService<TimedJobService>();
        }
        #endregion
    }
}
