using HockeyEventService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HockeyEventService.Infrastructure;

public class HockeyEventServiceDbContext : DbContext
{
    public HockeyEventServiceDbContext(DbContextOptions<HockeyEventServiceDbContext> options)
        : base(options) { }

    public DbSet<PlaceDbModel> Places { get; set; }

    public DbSet<EventDbModel> Events { get; set; }

    public DbSet<ChatRoomDbModel> ChatRooms { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
