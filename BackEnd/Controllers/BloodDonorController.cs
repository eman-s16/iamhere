using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppContext = WebApplicationAnaHon.Data.AppDbContext;
using AnahoneAPI.Models;
using WebApplicationAnaHon.Data.Models;
namespace WebApplicationAnaHon.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloodDonorController : ControllerBase
{
    private readonly AppContext _context; 

    public BloodDonorController(AppContext context)
    {
        _context = context;
    }


    [HttpGet("getall")]
    public async Task<ActionResult<IEnumerable<BloodDonor>>> GetBloodDonors()
    {
        return await _context.Donors.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BloodDonor>> GetBloodDonor(int id)
    {
        var bloodDonor = await _context.Donors.FindAsync(id);

        if (bloodDonor == null)
        {
            return NotFound();
        }

        return bloodDonor;
    }


    // POST: api/BloodDonors
        [HttpPost("Add")]
    public async Task<IActionResult> CreateBloodDonor([FromBody] BloodDonor doner)
    {
        if (doner == null)
        {
            return BadRequest("Invalid request body.");
        }

        // Check model state for validation errors
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
            return BadRequest(errors); // Return validation errors
        }

        try
        {
            // Add the provided person object to the database
            await _context.Donors.AddAsync(doner);
            await _context.SaveChangesAsync();

            // Return a custom success response
            var response = new
            {
                Success = true,
                Message = "Donor added successfully",
                Person = doner // Optionally return the added person object
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            // If an error occurs during database operation, return a custom error response
            var response = new
            {
                Success = false,
                Message = "Failed to add Donor",
                Error = ex.Message // Optionally include the error message
            };
            return StatusCode(500, response);
        }
    }
   
    [HttpGet("search")]
    public async Task<IActionResult> SearchDonors(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return BadRequest("Search keyword is required.");
        }

        var donors = await _context.Donors
            .Where(d =>
                d.FirstName.Contains(keyword) ||
                d.LastName.Contains(keyword) ||
                d.Gender.Contains(keyword) ||
                d.BloodType.Contains(keyword) ||
                d.Email.Contains(keyword))
            .ToListAsync();

        if (donors == null || donors.Count == 0)
        {
            return NotFound("No matching donors found.");
        }

        return Ok(donors);
    }








    private bool BloodDonorExists(int id)
    {
        return _context.Donors.Any(e => e.DonorID == id);
    }

}
