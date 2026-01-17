using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Interfaces.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class CartService(ICart cartInterface, 
        IMapper mapper, 
        IGeneric<Product> productInterface, 
        IPaymentMethodService paymentMethodService, 
        IPaymentService paymentService,
        IUserManagement userManagement) : ICartService
    {
        public async Task<ServiceResponse> Checkout(Checkout checkout)
        {
            var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethods = await paymentMethodService.GetPaymentMethods();

            if(checkout.PaymentMethodId == paymentMethods.FirstOrDefault().Id)
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            else
                return new ServiceResponse(false, "Invalid payment method");
        }

        public async Task<IEnumerable<GetAchieve>> GetAchieves()
        {
            var history = await cartInterface.GetAllCheckoutHistory();
            if (history == null) return [];
            var groupByCustomerId = history.GroupBy(x=>x.UserId).ToList();
            var products = await productInterface.GetAllAsync();
            var achieves = new List<GetAchieve>();
            foreach (var customerId in groupByCustomerId) 
            {
                var customerDetails = await userManagement.GetUserById(customerId.Key!);
                foreach(var item in customerId)
                {
                    var product = products.FirstOrDefault(x=>x.Id == item.ProductId);
                    achieves.Add(new GetAchieve
                    {
                        CustomerName = customerDetails.Fullname,
                        CustomerEmail = customerDetails.Email,
                        ProductName = product!.Name,
                        AmountPayed = item.Quantity * product.Price,
                        QuantityOrdered = item.Quantity,
                        DatePurchased = item.CreatedDate
                    });
                }
            }
            return achieves;
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var mappedData = mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await cartInterface.SaveCheckoutHistory(mappedData);
            return result > 0 ? new ServiceResponse(true, "Checkout achieved") : 
                new ServiceResponse(false, "Error occured in saving");
        }

        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if(!carts.Any()) return ([],0);

            var products = await productInterface.GetAllAsync();
            if(!products.Any()) return ([],0);

            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .ToList();

            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p=>p.Id==cartItem.ProductId)!.Price);
            return (cartProducts!,  totalAmount);
        }
    }
}
