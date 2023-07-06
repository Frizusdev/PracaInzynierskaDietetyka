using PracaInzynierskaDietetyka.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracaInzynierskaDietetyka.Entity;

namespace PracaInzynierskaDietetyka.Data;

public class ApplicationDbContext : IdentityDbContext<XUser, XRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Dishes> Dishes { get; set; }

    public DbSet<Workout> Work { get; set; }

    public DbSet<Users> User_Data { get; set; }

    public DbSet<Connector> Connector { get; set; }

    public DbSet<Dish_Types> Dish_Types { get; set; }

    public DbSet<WorkoutConnector> ConnectorWorkOut { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
