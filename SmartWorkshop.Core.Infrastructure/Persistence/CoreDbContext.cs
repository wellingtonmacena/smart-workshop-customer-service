using Microsoft.EntityFrameworkCore;
using SmartWorkshop.Core.Domain.Entities;

namespace SmartWorkshop.Core.Infrastructure.Persistence;

public class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    public DbSet<AvailableService> AvailableServices { get; set; }
    public DbSet<AvailableServiceSupply> AvailableServiceSupplies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Person Configuration
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("people");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fullname).HasColumnName("fullname").IsRequired().HasMaxLength(200);
            entity.Property(e => e.PersonType).HasColumnName("person_type").IsRequired();
            entity.Property(e => e.EmployeeRole).HasColumnName("employee_role");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            // Value Objects
            entity.OwnsOne(e => e.Document, doc =>
            {
                doc.Property(d => d.Value).HasColumnName("document").IsRequired().HasMaxLength(20);
            });

            entity.OwnsOne(e => e.Email, email =>
            {
                email.Property(e => e.Address).HasColumnName("email").IsRequired().HasMaxLength(100);
            });

            entity.OwnsOne(e => e.Phone, phone =>
            {
                phone.Property(p => p.AreaCode).HasColumnName("phone_area_code").HasMaxLength(3);
                phone.Property(p => p.Number).HasColumnName("phone_number").HasMaxLength(15);
            });

            entity.OwnsOne(e => e.Password, pwd =>
            {
                pwd.Property(p => p.Value).HasColumnName("password_hash").IsRequired();
            });

            entity.HasOne(e => e.Address)
                .WithOne(a => a.Person)
                .HasForeignKey<Person>(e => e.AddressId);

            entity.HasMany(e => e.Vehicles)
                .WithOne(v => v.Person)
                .HasForeignKey(v => v.PersonId);
        });

        // Address Configuration
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("addresses");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Street).HasColumnName("street").HasMaxLength(200);
            entity.Property(e => e.City).HasColumnName("city").HasMaxLength(100);
            entity.Property(e => e.State).HasColumnName("state").HasMaxLength(2);
            entity.Property(e => e.ZipCode).HasColumnName("zip_code").HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // Vehicle Configuration
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("vehicles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Model).HasColumnName("model").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Brand).HasColumnName("brand").IsRequired().HasMaxLength(100);
            entity.Property(e => e.ManufactureYear).HasColumnName("manufacture_year").IsRequired();
            entity.Property(e => e.PersonId).HasColumnName("person_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.OwnsOne(e => e.LicensePlate, lp =>
            {
                lp.Property(l => l.Value).HasColumnName("license_plate").IsRequired().HasMaxLength(10);
                lp.HasIndex(l => l.Value).IsUnique();
            });
        });

        // Supply Configuration
        modelBuilder.Entity<Supply>(entity =>
        {
            entity.ToTable("supplies");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnName("price").IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // AvailableService Configuration
        modelBuilder.Entity<AvailableService>(entity =>
        {
            entity.ToTable("available_services");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnName("price").IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // AvailableServiceSupply Configuration (Many-to-Many)
        modelBuilder.Entity<AvailableServiceSupply>(entity =>
        {
            entity.ToTable("available_service_supplies");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailableServiceId).HasColumnName("available_service_id").IsRequired();
            entity.Property(e => e.SupplyId).HasColumnName("supply_id").IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(e => e.AvailableService)
                .WithMany(a => a.AvailableServiceSupplies)
                .HasForeignKey(e => e.AvailableServiceId);

            entity.HasOne(e => e.Supply)
                .WithMany(s => s.AvailableServiceSupplies)
                .HasForeignKey(e => e.SupplyId);
        });
    }
}
