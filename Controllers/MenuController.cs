
using Microsoft.AspNetCore.Mvc;
using neat.Models.Entities;
using neat.Models.ViewModels;
using neat.Repositories;
//using neat.Models;

namespace neat.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuRepository repository;

        public MenuController(MenuRepository repository)
        {
            this.repository = repository;
        }

        [Route("Menu/{id?}")]
        public IActionResult Index(string id)
        {


            if (id != null)
            {
                id = id.Replace("-", " ");
                var hamburguesasList = repository
            .GetAll()
            .OrderBy(x => x.Nombre)
            .Select(x => new HamburguesaModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripción,
                Precio = x.Precio,
                Clasificacion = x.IdClasificacionNavigation.Nombre
            })
            .GroupBy(x => x.Clasificacion);

                var hbActual = repository.GetByNombre(id);

                var vm = new MenuViewModel()
                {
                    Hamburguesa = new HamburguesaModel()
                    {
                        Id = hbActual.Id,
                        Nombre = hbActual.Nombre,
                        Descripcion = hbActual.Descripción,
                        Precio = hbActual.Precio,
                        Clasificacion = hbActual.IdClasificacionNavigation.Nombre
                    },
                    Hamburguesas = hamburguesasList
                };
                return View(vm);
            }

            var hamburguesaActual = repository
                .GetAll()
                .Select(x => new HamburguesaModel()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripción,
                    Precio = x.Precio,
                    Clasificacion = x.IdClasificacionNavigation.Nombre
                })
                .FirstOrDefault();

            var hamburguesas = repository
                .GetAll()
                .OrderBy(x => x.Nombre)
                .Select(x => new HamburguesaModel()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripción,
                    Precio = x.Precio,
                    Clasificacion = x.IdClasificacionNavigation.Nombre
                })
                .GroupBy(x => x.Clasificacion);

            var model = new MenuViewModel()
            {
                Hamburguesa = hamburguesaActual,
                Hamburguesas = hamburguesas
            };

            return View(model);
        }




    }
}