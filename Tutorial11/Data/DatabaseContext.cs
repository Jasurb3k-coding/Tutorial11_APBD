using Microsoft.EntityFrameworkCore;
using Tutorial11.Models;

namespace Tutorial11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Jane", LastName = "Doe", Email = "jan.d  z`" },
            new Doctor() { IdDoctor = 2, FirstName = "Hamilton", LastName = "Duran", Email = "ham.dur@gam.com" },
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient()
                { IdPatient = 1, FirstName = "James", LastName = "Bond", BirthDate = new DateOnly(1990, 1, 1) },
            new Patient()
                { IdPatient = 2, FirstName = "Arthur", LastName = "Theking", BirthDate = new DateOnly(1990, 1, 1) },
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "Paracetamol", Description = "Paracetamol", Type = "Tablet" },
            new Medicament() { IdMedicament = 2, Name = "Ibuprofen", Description = "Ibuprofen", Type = "Tablet" },
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription()
            {
                IdPrescription = 1, Date = new DateOnly(2025, 5, 24), DueDate = new DateOnly(2025, 5, 24), IdDoctor = 1,
                IdPatient = 1
            },
            new Prescription()
            {
                IdPrescription = 2, Date = new DateOnly(2025, 5, 24), DueDate = new DateOnly(2025, 5, 24), IdDoctor = 1,
                IdPatient = 2
            },
            new Prescription()
            {
                IdPrescription = 3, Date = new DateOnly(2025, 5, 24), DueDate = new DateOnly(2025, 5, 24), IdDoctor = 2,
                IdPatient = 2
            },
        });
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament() { IdPrescription = 1, IdMedicament = 1, Dose = 10, Details = "10mg" },
            new PrescriptionMedicament() { IdPrescription = 1, IdMedicament = 2, Dose = 20, Details = "20mg" },
            new PrescriptionMedicament() { IdPrescription = 2, IdMedicament = 1, Dose = 30, Details = "30mg" },
            new PrescriptionMedicament() { IdPrescription = 2, IdMedicament = 2, Dose = 40, Details = "40mg" },
            new PrescriptionMedicament() { IdPrescription = 3, IdMedicament = 1, Dose = 50, Details = "50mg" },
        });
    }
}