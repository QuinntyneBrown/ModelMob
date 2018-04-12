using Microsoft.EntityFrameworkCore;
using ModelMob.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelMob.Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<Model> Models { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Model> Models { get; set; }
    }
}
