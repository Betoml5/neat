

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

            if (Id != null)
            {
                Id = Id.Replace("-", " ");

            }

            var promociones = repository
                .GetAll()
                .Where(x => x.PrecioPromocion > 0)
                .Select(x => new HamburguesaModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripción,
                    Precio = x.Precio,
                    PrecioPromocion = x.PrecioPromocion ?? 0,
                });

            var model = new PromocionesViewModel()
            {
                IndiceModeloActual = 0,
                Hamburguesas = promociones
            };


            return View(model);
        }
    }
}