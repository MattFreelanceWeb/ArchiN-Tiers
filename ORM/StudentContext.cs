using Microsoft.EntityFrameworkCore;
using Models;

namespace ORM
{
    public partial class StudentContext : DbContext
    {
		private static readonly object _padlock = new object();
		private static StudentContext? _instance;
		//méthode statique qui renvoit une instance de ConDB
		public static StudentContext Instance
		{
			get
			{
				//check pour éviter l'accés multi-thread
				lock(_padlock)
				{
					if(_instance == null) //si null on instancié
						_instance = new StudentContext();
					return _instance;
				}
			}
		}

		public StudentContext()
		{
		}

		public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

		public virtual DbSet<Student> Student { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
	            optionsBuilder.UseNpgsql("Host=localhost;Database=student;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
