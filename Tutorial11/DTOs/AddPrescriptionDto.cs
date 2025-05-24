namespace Tutorial11.DTOs;

public class AddPrescriptionDto
{
    public AddPrescription_PatientDto Patient { get; set; }
    public List<AddPrescription_MedicamentDto> Medicaments { get; set; }
    public int IdDoctor { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}

public class AddPrescription_PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}

public class AddPrescription_MedicamentDto
{
    public int IdMedicament { get; set; }
    public string Description { get; set; }
    public int? Dose { get; set; }
}