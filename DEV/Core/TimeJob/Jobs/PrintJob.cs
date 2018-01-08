using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TimeJob.Jobs
{
    public class PrintJob : Job
    {
        public void Print()
        {
            Console.WriteLine("Test dynamic invoke...");
        }
    }
}
