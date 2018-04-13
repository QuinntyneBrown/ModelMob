using Microsoft.EntityFrameworkCore;
using ModelMob.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ModelMob.Infrastructure.Data
{
    public interface IModelMobDbContext
    {
        DbSet<Model> Models { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class ModelMobDbContext: DbContext, IModelMobDbContext
    {
        public DbSet<Model> Models { get; set; }
    }
}
