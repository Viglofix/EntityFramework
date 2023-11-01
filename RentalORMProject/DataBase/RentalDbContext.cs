using DataBase.Models;
using Microsoft.EntityFrameworkCore;

// 1. You need to set up keys as a int because long wich is representation of BIGINT is nonnullable so
// in many cases error will be issued
// 2. HasIndex is not equal to HasKey. Indexers are used to improve efficience of database records schearching
// 3. ~ operator is comparison operator. It is used in PostgreeSQL to compare specified regular expression
// to given pattern. We dont use square brackets at all in current context. {} I suppose are allowed...

namespace DataBase;

public class RentalDbContext : DbContext
{
    
    public const string Connection = "Server=localhost;Database=RentalORM;Port=5432;User Id=viglofix;Password=Hujbert12";
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
    { 
        
    }
    public virtual DbSet<Rental> Rentals { get; set; }
    public virtual DbSet<User> Users { get; set; }


  /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Connection); 
        base.OnConfiguring(optionsBuilder);
    } */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Rental Primary Key
        modelBuilder.Entity<Rental>()
            .HasKey(x => x.id);
      /*  modelBuilder.Entity<User>()
            .HasKey(x => x.id); */

        // Rental Table
        modelBuilder.Entity<Rental>()
            .Property(x => x.id)
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions(startValue: 10, incrementBy: 10, maxValue: 100, minValue: 10, cyclic: true)
            .IsRequired();
        modelBuilder.Entity<Rental>()
            .Property(x => x.user_id)
            .HasColumnType("BIGINT")
            .HasColumnName("user_id")
            .HasDefaultValue(null)
            .IsRequired(false);
      /* modelBuilder.Entity<Rental>()
            .Property(x => x.vehicle_id)
            .HasColumnType("BIGINT")
            .HasColumnName("vehicle_id")
            .HasDefaultValue(null)
            .IsRequired(false);
        modelBuilder.Entity<Rental>()
            .Property(x => x.payment_id)
            .HasColumnType("BIGINT")
            .HasColumnName("vehicle_id")
            .HasDefaultValue(null)
            .IsRequired(false); */
        modelBuilder.Entity<Rental>()
            .Property(x => x.start_date)
            .HasColumnType("DATE")
            .HasColumnName("start")
            .HasDefaultValue("2023-01-01")
            .IsRequired();
        modelBuilder.Entity<Rental>()
            .Property(x => x.end_date)
            .HasColumnType("DATE")
            .HasColumnName("end")
            .HasDefaultValue("2023-01-30")
            .IsRequired();
        modelBuilder.Entity<Rental>()
            .Property(x => x.total_cost)
            .HasColumnType("NUMERIC")
            .HasPrecision(5, 2)
            .HasDefaultValue(5000.00M)
            .IsRequired();
        // User Table 
        modelBuilder.Entity<User>()
            .Property(x => x.first_name)
            .HasColumnName("first_name")
            .HasMaxLength(50)
            .HasColumnType("VARCHAR")
            .HasDefaultValue("Janusz")
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.last_name)
            .HasColumnName("last_name")
            .HasMaxLength(50)
            .HasColumnType("VARCHAR")
            .HasDefaultValue("Kowalski")
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.gender)
            .HasColumnType("VARCHAR")
            .HasColumnName("gender")
            .HasDefaultValue("m")
            .HasMaxLength(1)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.email)
            .HasColumnName("email")
            .HasColumnType("TEXT")
            .HasDefaultValue("example@email.com")
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.address)
            .HasColumnName("address")
            .HasColumnType("TEXT")
            .HasDefaultValue("Example 123")
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.phone_no)
            .HasColumnName("phone")
            .HasColumnType("VARCHAR")
            .HasDefaultValue("888 993 123")
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.password)
            .HasColumnName("password")
            .HasColumnType("TEXT")
            .HasDefaultValue("dupa12")
            .IsRequired();


        modelBuilder.Entity<User>()
            .HasCheckConstraint("EmailREGEXCheck", @"email ~ '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$'");
        modelBuilder.Entity<User>()
            .HasCheckConstraint("GenderREGEXCheck", @"gender ~ '^[mf]{1}$'");
        modelBuilder.Entity<User>()
            .HasCheckConstraint("AddressREGEXCheck", @"address ~ '^[A-Za-z]+\s[0-9]{3}$'"); 


        modelBuilder.Entity<User>()
            .HasCheckConstraint("phoneREGEXCheck", @"phone ~ '^\d{3}\s?\d{3}\s?\d{3}$'"); // phone_no has changed during compilation process to phone
        modelBuilder.Entity<User>()
            .HasCheckConstraint("passwordREGEXCheck", @"password ~ '^[A-Za-z0-9@_]{6,}$'");

        // Relations

        modelBuilder.Entity<Rental>()
            .HasOne<User>(x => x.User)
            .WithMany(x => x.Rentals)
            .HasForeignKey(x => x.user_id);

        base.OnModelCreating(modelBuilder);
    }
}
