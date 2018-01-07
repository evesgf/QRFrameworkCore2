using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QRFrameworkCore2.Data.Entity;

namespace QRFrameworkCore2.Data
{
    /// <summary>
    /// http://blog.csdn.net/linux12a/article/details/77480111
    /// 生成数据库架构：Add-Migration MyFirstMigration
    /// 指定迁移：Update-Database –TargetMigration: 20180107124745_MyFirstMigration
    /// 更新数据库：Update-Database
    /// 自动迁移：https://www.cnblogs.com/stulzq/p/7729380.html
    /// </summary>
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }

        public DbSet<SysUser> SysUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysUser>().ToTable("SysUser");
        }
    }
}
