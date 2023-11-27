


using Microsoft.EntityFrameworkCore;
using neat.Models.Entities;
using neat.Repositories;

namespace neat.Repositories
{

    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext ctx) : base(ctx)
        {
        }

        override
         public IEnumerable<Menu> GetAll()
        {
            return ctx.Menu
                .OrderBy(x => x.Nombre)
                .Include(x => x.IdClasificacionNavigation);
        }

        public Menu? GetByNombre(string nombre)
        {
            return ctx.Menu
                .Include(x => x.IdClasificacionNavigation)
                .FirstOrDefault(x => x.Nombre == nombre);
        }
    }
}