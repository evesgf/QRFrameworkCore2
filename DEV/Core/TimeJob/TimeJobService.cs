
using Core.Common;
using Microsoft.EntityFrameworkCore;
using Pomelo.AspNetCore.TimedJob.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TimeJob
{
    public class TimeJobService : ITimeJobService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimeJobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddTestTimeJob()
        {
            var mode = new TimedJob
            {
                Id = "Core.TimeJob.Jobs.PrintJob.Print", // 按照完整类名+方法形式填写
                Begin = DateTime.Now,
                Interval = 3000,
                IsEnabled = true
            };// 添加一个定时事务
            var re=_unitOfWork.GetRepository<TimedJob>();
            var a = re.GetFirstOrDefault(b => new { Id=b.Id }, predicate: x => x.Id.Contains(mode.Id));

            if (a == null)
            {
                re.Insert(mode);
                _unitOfWork.SaveChanges();

                // 增删改过数据库事务后需要重启动态定时器
                Singleton<TimeJobManager>.Instance.timedJobService.RestartDynamicTimers();

                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
