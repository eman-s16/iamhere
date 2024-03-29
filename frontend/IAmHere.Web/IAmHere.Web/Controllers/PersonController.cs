using IAmHere.Web.Models.Person;
using IAmHere.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IAmHere.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;
        private readonly ILogger<HomeController> _logger;

        public PersonController(ILogger<HomeController> logger, IPersonRepository personRepo)
        {
            _logger = logger;
            _personRepo = personRepo;
        }

       
        public IActionResult SearchPersons(int page = 1, string searchString = "")
        {
            int pageSize = 9;
            List<PersonModel> list = _personRepo.GetAll();

            // Filter by name if a search string is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(p => p.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    p.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var paginatedList = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)list.Count / pageSize);
            ViewBag.SearchString = searchString;

            return View(paginatedList);

        }
        public IActionResult AddPerson()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddPerson(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Attempt to add the person
                    var response = await _personRepo.Add(personModel);

                    if (response.Success)
                    {
                        // If person is added successfully, return a success message
                        return Json(new { success = true, message = "Person added successfully" });
                    }
                    else
                    {
                        // If there's an error adding the person, return an error message
                        return Json(new { success = false, message = response.Message });
                    }
                }
                catch (Exception ex)
                {
                    // If an exception occurs, return an error message
                    return Json(new { success = false, message = "An error occurred while processing your request." });
                }
            }
            else
            {
                // If model state is not valid, return an error message
                return Json(new { success = false, message = "Invalid form data." });
            }
        }



    }
}
