using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.AspNetCore.TimedJob;

namespace API
{
    public static class SampleData
    {
        public static void InitDB(IServiceProvider services)
        {
            var DB = services.GetRequiredService<MySqlDbContext>();
            var TimedJobService = services.GetRequiredService<TimedJobService>();
            DB.Database.EnsureCreated();
            DB.TimedJobs.Add(new Pomelo.AspNetCore.TimedJob.EntityFramework.TimedJob
            {
                Id = "Core.TimeJob.Jobs.PrintJob.Print", // 按照完整类名+方法形式填写
                Begin = DateTime.Now,
                Interval = 3000,
                IsEnabled = true
            }); // 添加一个定时事务
            DB.SaveChanges();
            TimedJobService.RestartDynamicTimers(); // 增删改过数据库事务后需要重启动态定时器
        }
    }
}
