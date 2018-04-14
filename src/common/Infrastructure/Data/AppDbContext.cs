using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<Booking> Bookings { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<ContactRequest> ContactRequests { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<FeaturedModel> FeaturedModels { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<FeaturedModel> FeaturedModels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
