
namespace neat.Models.ViewModels
{
    public class MenuViewModel
    {
        public HamburguesaModel Hamburguesa { get; set; } = null!;
        public IEnumerable<IGrouping<string, HamburguesaModel>> Hamburguesas { get; set; } = null!;
    }


}
