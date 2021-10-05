using Microsoft.EntityFrameworkCore;
using CitasMedicas.Models;

namespace CitasMedicas.Data
{
    public class CitasMedicasContext: DbContext
    {

        public CitasMedicasContext(DbContextOptions<CitasMedicasContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {              
        }


    }
}