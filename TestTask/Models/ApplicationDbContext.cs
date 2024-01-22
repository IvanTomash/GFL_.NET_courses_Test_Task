using Microsoft.EntityFrameworkCore;

namespace TestTask.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(builder =>
            {
                builder.ToTable("Phones");

                builder.HasKey(x => x.Id);

                builder.Property(x => x.Id)
                .UseHiLo("phones_hilo")
                .IsRequired();

                builder.Property(x => x.Mobile).IsRequired();
                builder.Property(x => x.Home).IsRequired();
            });

            modelBuilder.Entity<Job>(builder =>
            {
                builder.ToTable("Job");

                builder.HasKey(x => x.Id);

                builder.Property(x => x.Id)
                .UseHiLo("job_hilo")
                .IsRequired();

                builder.Property(x => x.Position).IsRequired();
                builder.Property(x => x.Experience).IsRequired();
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User");

                builder.Property(x => x.Id)
                .UseHiLo("user_hilo")
                .IsRequired();

                builder.Property(x => x.Name).IsRequired();
                builder.Property(x => x.Surname).IsRequired();
                builder.Property(x => x.Age).IsRequired();

                builder.HasOne(x => x.Job)
                .WithMany()
                .HasForeignKey(x => x.JobId);

                builder.HasOne(x => x.Phones)
                .WithMany()
                .HasForeignKey(x => x.PhonesId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SNEGPAD\SQL2023;Initial Catalog=TestTask;Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True;");
        }
    }
}
