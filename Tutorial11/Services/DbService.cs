using Microsoft.EntityFrameworkCore;
using Tutorial11.Data;
using Tutorial11.DTOs;
using Tutorial11.Models;

namespace Tutorial11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PatientDetailDto?> getPatient(int id)
    {
        var patient = await _context.Patients.Where(p => p.IdPatient == id).Select(e =>
            new PatientDetailDto
            {
                IdPatient = e.IdPatient,
                FirstName = e.FirstName,
                Prescriptions = e.Prescriptions.Select(p => new PrescriptionDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date.ToString(),
                    DueDate = p.DueDate.ToString(),
                    Medicaments = p.PrescriptionMedicaments.Select(m => new MedicamentDto
                    {
                        IdMedicament = m.IdMedicament,
                        Name = m.Medicament.Name,
                        Description = m.Medicament.Description,
                        Dose = m.Dose
                    }).ToList(),
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                    }
                }).OrderBy(i => i.DueDate).ToList()
            }).FirstOrDefaultAsync();
        return patient;
    }


    public async Task<int> addPrescription(AddPrescriptionDto prescription)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var patient = await _context.Patients.Where(p => p.IdPatient == prescription.Patient.IdPatient)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = prescription.Patient.FirstName,
                    LastName = prescription.Patient.LastName,
                    BirthDate = prescription.Patient.BirthDate
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }


            var medicationIds = prescription.Medicaments.Select(m => m.IdMedicament).ToList();

            var medications = await _context.Medicaments
                .Where(m => medicationIds.Contains(m.IdMedicament))
                .ToListAsync();

            if (medications.Count != prescription.Medicaments.Count)
            {
                throw new Exception("Medicaments not found");
            }


            var newPrescription = new Prescription
            {
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                IdDoctor = prescription.IdDoctor,
                IdPatient = patient.IdPatient
            };
            _context.Prescriptions.Add(newPrescription);
            await _context.SaveChangesAsync();

            foreach (var medication in prescription.Medicaments)
            {
                var newPrescriptionMedicament = new PrescriptionMedicament
                {
                    IdPrescription = newPrescription.IdPrescription,
                    IdMedicament = medication.IdMedicament,
                    Dose = medication.Dose,
                    Details = medication.Description,
                };
                _context.PrescriptionMedicaments.Add(newPrescriptionMedicament);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return newPrescription.IdPrescription;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}