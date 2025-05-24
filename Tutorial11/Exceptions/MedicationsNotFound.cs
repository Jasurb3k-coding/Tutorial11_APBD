namespace Tutorial11.Exceptions;

public class MedicationsNotFound : Exception
{
    public MedicationsNotFound() : base("Medicaments not found")
    {
    }
}