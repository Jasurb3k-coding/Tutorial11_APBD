using Microsoft.AspNetCore.Mvc;
using Tutorial11.DTOs;
using Tutorial11.Services;

namespace Tutorial11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddPrescriptionDto prescription)
    {
        if (prescription.Medicaments.Count > 10) return BadRequest("Max 10 medicaments per prescription");
        if (prescription.DueDate < prescription.Date) return BadRequest("Due date must be greater than current date");
        try
        {
            var id = await _dbService.addPrescription(prescription);
            return Ok(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}