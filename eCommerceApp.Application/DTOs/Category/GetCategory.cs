using eCommerceApp.Application.DTOs.Product;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { set; get; }
        public ICollection<GetProduct>? Products { get; set; } 
    }
}
