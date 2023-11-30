
namespace neat.Areas.Admin.Models.ViewModels
{
    public class AgregarPromocionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double PrecioReal { get; set; }
        public double PrecioPromocion { get; set; }
    }
}