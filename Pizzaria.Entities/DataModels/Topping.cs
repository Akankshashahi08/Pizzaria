
namespace Pizzaria.Entities.DataModels
{
    public class Topping : IDbEntity
    {
        public int ToppingId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
