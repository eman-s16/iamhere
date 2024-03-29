using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAnaHon.Data;
using WebApplicationAnaHon.Data.Models;

namespace WebApplicationAnaHon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public PersonController(AppDbContext db)
        {
            _db = db;
        }
        private readonly AppDbContext _db;
        
      
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPersons()
        {
            var person = await _db.Persons.ToListAsync();
            return Ok(person);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _db.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddPerson([FromBody] MissingPersons person)
        {
            if (person == null)
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
                await _db.Persons.AddAsync(person);
                await _db.SaveChangesAsync();

                // Return a custom success response
                var response = new
                {
                    Success = true,
                    Message = "Person added successfully",
                    Person = person // Optionally return the added person object
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an error occurs during database operation, return a custom error response
                var response = new
                {
                    Success = false,
                    Message = "Failed to add person",
                    Error = ex.Message // Optionally include the error message
                };
                return StatusCode(500, response);
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchPersons(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Search keyword is required.");
            }

            var persons = await _db.Persons
                .Where(p =>
                    p.FirstName.Contains(keyword) ||
                    p.LastName.Contains(keyword) ||
                    p.Age.ToString().Contains(keyword) ||
                    p.Gender.Contains(keyword) ||
                    p.LastSeenDate.ToString().Contains(keyword) ||
                    p.Location.Contains(keyword) ||
                    p.Description.Contains(keyword) ||
                    p.CaseDetails.Contains(keyword) ||
                    p.ClothingDescription.Contains(keyword) ||
                    p.HeightCM.ToString().Contains(keyword) ||
                    p.WeightKG.ToString().Contains(keyword) ||
                    p.HairColor.Contains(keyword) ||
                    p.EyeColor.Contains(keyword) ||
                    p.PhotoPath.Contains(keyword) ||
                    p.Contact.Contains(keyword) ||
                    p.CaseStatus.Contains(keyword))
                .ToListAsync();

            if (persons == null || persons.Count == 0)
            {
                return NotFound("No matching persons found.");
            }

            return Ok(persons);
        }


    }
}
