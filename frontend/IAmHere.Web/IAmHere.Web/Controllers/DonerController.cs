using IAmHere.Web.Models.Doner;
using IAmHere.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IAmHere.Web.Controllers
{
    public class DonerController : Controller
    {
        private readonly IDonerRepository _DonerRepo;
        private readonly ILogger<DonerController> _logger;

        public DonerController(ILogger<DonerController> logger, IDonerRepository doner)
        {
            _logger = logger;
            _DonerRepo = doner;
        }


        public IActionResult SearchDoners(int page = 1, string searchString = "")
        {
            int pageSize = 9;
            List<DonerModel> list = _DonerRepo.GetAll();

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
        public IActionResult AddDoner()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddDoner(DonerModel DonerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Attempt to add the Doner
                    var response = await _DonerRepo.Add(DonerModel);

                    if (response.Success)
                    {
                        // If Doner is added successfully, return a success message
                        return Json(new { success = true, message = "Doner added successfully" });
                    }
                    else
                    {
                        // If there's an error adding the Doner, return an error message
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
                return View();
            }
        }

    }
}
