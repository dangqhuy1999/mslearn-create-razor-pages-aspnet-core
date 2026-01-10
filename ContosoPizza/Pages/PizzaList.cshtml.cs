using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get;set; } = default!;

        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;
        
        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);

            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            // This forces the browser to do a fresh GET request, 
            // which triggers OnGet() and refills the PizzaList.
            //return RedirectToPage();
            return RedirectToAction("Get");
        }
    }
}
