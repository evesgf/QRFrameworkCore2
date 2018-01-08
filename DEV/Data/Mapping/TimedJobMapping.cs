using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomelo.AspNetCore.TimedJob.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class TimedJobMapping
    {
        public TimedJobMapping(EntityTypeBuilder<TimedJob> entityBuilder)
        {
            entityBuilder.Property(x => x.Id).HasMaxLength(128);
        }
    }
}
