
namespace neat.Models.ViewModels
{
    public class PromocionesViewModel
    {

        public int IndiceModeloActual { get; set; } = 0;
        public List<HamburguesaModel> Hamburguesas { get; set; } = null!;

    }

    public class HamburguesaModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public double PrecioPromocion { get; set; } = 0;
        public string Clasificacion { get; set; } = null!;


    }

}