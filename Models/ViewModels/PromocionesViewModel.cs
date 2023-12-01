
using neat.Models.Entities;

namespace neat.Models.ViewModels
{
    public class PromocionesViewModel
    {


        public Menu? Hamburguesa { get; set; }
        public Menu Siguiente { get; set; }
        public Menu Anterior { get; set; }

    }

    public class HamburguesaModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public double PrecioPromocion { get; set; } = 0;
        public string? Clasificacion { get; set; } = null!;


    }

}