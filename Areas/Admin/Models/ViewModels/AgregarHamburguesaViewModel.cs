

namespace neat.Areas.Admin.Models.ViewModels
{
    public class AgregarHamburguesaViewModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }

        public int IdClasificacion { get; set; }

        public IEnumerable<ClasificacionModel>? Clasificaciones { get; set; }

        public IFormFile? Imagen { get; set; } = null!;
    }

    public class ClasificacionModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}