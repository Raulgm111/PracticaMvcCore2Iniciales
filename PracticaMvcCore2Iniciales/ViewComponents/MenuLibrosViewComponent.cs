using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.ViewComponents
{
    public class MenuLibrosViewComponent: ViewComponent
    {
        private RepositoryLibros repo;

        public MenuLibrosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> genero = this.repo.GetGeneros();
            return View(genero);
        }
    }
}
