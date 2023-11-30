

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

        [Route("Promociones/{id?}")]
        public IActionResult Index(string Id)
        {

            var hamburguesasPromocion = repository
                .GetAll()
                .Where(x => x.PrecioPromocion > 0);
            if (hamburguesasPromocion != null)
            {
                var IdPrimerHamburguesa = hamburguesasPromocion.FirstOrDefault().Id;


                var vm = new PromocionesViewModel()
                {
                    IndiceModeloActual = IdPrimerHamburguesa,
                    Hamburguesas = hamburguesasPromocion.Select(x => new HamburguesaModel()
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Descripcion = x.Descripción,
                        Precio = x.Precio,
                        PrecioPromocion = (double)x.PrecioPromocion,

                    }).ToList()
                };


                return View(vm);

            }

            return View(new PromocionesViewModel() { Hamburguesas = new List<HamburguesaModel>() });
        }
    }
}