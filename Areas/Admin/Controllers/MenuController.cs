

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using neat.Areas.Admin.Models.ViewModels;
using neat.Models.Entities;
using neat.Repositories;
//using neat.Areas.Admin.Models;

namespace neat.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly MenuRepository menuRepository;
        private readonly Repository<Clasificacion> clasificacionRepository;

        public MenuController(MenuRepository menuRepository, Repository<Clasificacion> clasificacionRepository)
        {
            this.menuRepository = menuRepository;
            this.clasificacionRepository = clasificacionRepository;
        }


        public IActionResult Index()
        {

            var vm = menuRepository.GetAll()
                .Select(x => new IndexMenuAdminViewModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripción,
                    Precio = x.Precio,
                    PrecioPromocion = x.PrecioPromocion ?? 0,
                    Clasificacion = x.IdClasificacionNavigation.Nombre
                })
                .GroupBy(x => x.Clasificacion);

            return View(vm);
        }

        public IActionResult Agregar()
        {
            var clasificaciones = clasificacionRepository.GetAll()
                .Select(x => new ClasificacionModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
            var vm = new AgregarHamburguesaViewModel
            {
                Clasificaciones = clasificaciones
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(AgregarHamburguesaViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es requerido");
            }
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("Descripcion", "La descripción es requerida");
            }
            if (vm.Precio == 0)
            {
                ModelState.AddModelError("Precio", "El precio es requerido");
            }
            if (vm.IdClasificacion == 0)
            {
                ModelState.AddModelError("Clasificacion", "La clasificación es requerida");
            }

            if (ModelState.IsValid)
            {
                var hamburguesa = new Menu
                {
                    Nombre = vm.Nombre,
                    Descripción = vm.Descripcion,
                    Precio = vm.Precio,
                    IdClasificacion = vm.IdClasificacion
                };

                menuRepository.Insert(hamburguesa);

                if (vm.Imagen == null)
                {
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{hamburguesa.Id}.png");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{hamburguesa.Id}.png");
                    vm.Imagen.CopyTo(fs);
                    fs.Close();
                }

                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }
            vm.Clasificaciones = clasificacionRepository.GetAll()
                .Select(x => new ClasificacionModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });

            return View(vm);
        }

        public IActionResult Eliminar(int Id)
        {
            var hamburguesa = menuRepository.Get(Id);
            if (hamburguesa != null)
            {
                var vm = new EliminarHamburguesaViewModel
                {
                    Id = hamburguesa.Id,
                    Nombre = hamburguesa.Nombre
                };
                return View(vm);

            }

            return RedirectToAction("Index", "Menu", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult Eliminar(EliminarHamburguesaViewModel vm)
        {
            var hamburguesa = menuRepository.Get(vm.Id);
            if (hamburguesa != null)
            {
                menuRepository.Delete(hamburguesa);
            }
            return RedirectToAction("Index", "Menu", new { area = "Admin" });

        }

        public IActionResult Editar(int Id)
        {
            var hamburguesa = menuRepository.Get(Id);
            var clasificaciones = clasificacionRepository.GetAll()
                .Select(x => new ClasificacionModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
            if (hamburguesa != null)
            {
                var vm = new AgregarHamburguesaViewModel
                {
                    Id = hamburguesa.Id,
                    Nombre = hamburguesa.Nombre,
                    Descripcion = hamburguesa.Descripción,
                    Precio = hamburguesa.Precio,
                    IdClasificacion = hamburguesa.IdClasificacion,
                    Clasificaciones = clasificaciones
                };
                return View(vm);

            }
            // TODO: Your code here
            return RedirectToAction("Index", "Menu", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult Editar(AgregarHamburguesaViewModel vm)
        {
            var clasificaciones = clasificacionRepository.GetAll()
                       .Select(x => new ClasificacionModel
                       {
                           Id = x.Id,
                           Nombre = x.Nombre
                       });
            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es requerido");
            }
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("Descripcion", "La descripción es requerida");
            }

            if (vm.Precio == 0)
            {
                ModelState.AddModelError("Precio", "El precio es requerido");
            }

            if (vm.IdClasificacion == 0)
            {
                ModelState.AddModelError("Clasificacion", "La clasificación es requerida");
            }

            if (ModelState.IsValid)
            {
                var hamburguesa = menuRepository.Get(vm.Id);
                if (hamburguesa != null)
                {
                    hamburguesa.Nombre = vm.Nombre;
                    hamburguesa.Descripción = vm.Descripcion;
                    hamburguesa.Precio = vm.Precio;
                    hamburguesa.IdClasificacion = vm.IdClasificacion;

                    menuRepository.Update(hamburguesa);

                    if (vm.Imagen != null)
                    {
                        System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{hamburguesa.Id}.png");
                        vm.Imagen.CopyTo(fs);
                        fs.Close();
                    }
                }
                return RedirectToAction("Index", "Menu", new { area = "Admin" });
            }

            vm.Clasificaciones = clasificaciones;

            return View(vm);
        }


        public IActionResult AgregarPromocion(int Id)
        {
            var hamburguesa = menuRepository.Get(Id);
            if (hamburguesa != null)
            {
                var vm = new AgregarPromocionViewModel
                {
                    Id = hamburguesa.Id,
                    Nombre = hamburguesa.Nombre,
                    PrecioReal = hamburguesa.Precio,
                    PrecioPromocion = hamburguesa.PrecioPromocion ?? 0
                };
                return View(vm);
            }
            return RedirectToAction("Index", "Menu", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult AgregarPromocion(AgregarPromocionViewModel vm)
        {
            var hamburguesa = menuRepository.Get(vm.Id);
            if (hamburguesa != null)
            {
                if (vm.PrecioPromocion < 0)
                {
                    ModelState.AddModelError("", "El precio de promoción no puede ser menor a 0");
                }

                if (vm.PrecioPromocion > vm.PrecioReal)
                {
                    ModelState.AddModelError("", "El precio de promoción no puede ser mayor al precio real");
                }

                if (ModelState.IsValid)
                {
                    hamburguesa.PrecioPromocion = vm.PrecioPromocion;
                    menuRepository.Update(hamburguesa);
                    return RedirectToAction("Index", "Menu", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "Menu", new { area = "Admin" });
        }

        public IActionResult QuitarPromocion(int Id)
        {
            // TODO: Your code here
            return View();
        }

        [HttpPost]
        public IActionResult QuitarPromocion(AgregarPromocionViewModel vm)
        {
            return View();
        }



    }
}