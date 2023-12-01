

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using neat.Models.Entities;
using neat.Models.ViewModels;
using neat.Repositories;
//using neat.Models;

namespace neat.Controllers
{
    public class PromocionesController : Controller
    {
        private readonly Repository<Menu> repository;

        public PromocionesController(Repository<Menu> repository)
        {
            this.repository = repository;
        }

        [Route("promociones/{Id?}")]
        public IActionResult Index(string? Id)
        {

            Menu? hamburguesa = new Menu();
            var hamburguesasEnPromocion = repository
                          .GetAll()
                          .Where(x => x.PrecioPromocion > 0);


            if (Id != null)
            {
                var nombreHamburguesa = Id.Replace("-", " ");
                hamburguesa = hamburguesasEnPromocion
                    .FirstOrDefault(x => x.Nombre == nombreHamburguesa);

                var next = hamburguesasEnPromocion
                    .SkipWhile(x => x.Id != hamburguesa.Id)
                    .Skip(1)
                    .FirstOrDefault();

                var previous = hamburguesasEnPromocion
                    .TakeWhile(x => x.Id != hamburguesa.Id)
                    .LastOrDefault();

                var vm2 = new PromocionesViewModel()
                {
                    Hamburguesa = hamburguesa,
                    Siguiente = next,
                    Anterior = previous
                };

                return View(vm2);

            }




            hamburguesa = hamburguesasEnPromocion
                .FirstOrDefault();

            var siguiente = hamburguesasEnPromocion
                .SkipWhile(x => x.Id != hamburguesa.Id)
                .Skip(1)
                .FirstOrDefault();

            var anterior = hamburguesasEnPromocion
                .TakeWhile(x => x.Id != hamburguesa.Id)
                .LastOrDefault();



            var vm = new PromocionesViewModel()
            {
                Hamburguesa = hamburguesa,
                Siguiente = siguiente,
                Anterior = anterior
            };


            return View(vm);
        }
    }
}