using Tutorial11.DTOs;

namespace Tutorial11.Services;

public interface IDbService
{
    Task<PatientDetailDto?> getPatient(int id);
    Task<int> addPrescription(AddPrescriptionDto prescription);
}