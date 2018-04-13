﻿using Microsoft.EntityFrameworkCore;
using ModelMob.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ModelMob.Infrastructure.Data
{
    public interface IModelMobDbContext
    {
        DbSet<Booking> Bookings { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<FeaturedModel> FeaturedModels { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class ModelMobDbContext: DbContext, IModelMobDbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<FeaturedModel> FeaturedModels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
