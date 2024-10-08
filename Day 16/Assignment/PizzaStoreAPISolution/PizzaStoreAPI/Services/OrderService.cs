using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interface;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Repositories;
using PizzaStoreAPI.Respositories;

namespace PizzaStoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly I _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<Order> PlaceOrder(int customerId)
        {
            var cart = (await _cartRepository.GetAll()).FirstOrDefault(c => c.CustomerId == customerId);
            if (cart == null) throw new NoEntityFoundException("Cart", customerId);

            var order = new Order
            {
                CustomerId = customerId,
                TotalAmount = cart.Pizzas.Sum(p => p.Price * p.Quantity),
                PaymentMethod = "Credit Card",
                IsPaymentSuccess = true,
                OrderStatus = OrderStatus.Success
            };

            await _orderRepository.Add(order);

            foreach (var pizza in cart.Pizzas)
            {
                var orderDetails = new OrderDetails
                {
                    OrderNumber = order.OrderNumber,
                    PizzaId = pizza.Id,
                    Quantity = pizza.Quantity,
                    Price = pizza.Price
                };

                await _orderDetailsRepository.Add(orderDetails);
            }

            // Clear the cart after ordering
            await _cartRepository.Delete(cart.CartNumber);

            return order;
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetOrderDetails(int orderNumber)
        {
            var orderDetails = await _orderDetailsRepository.GetAll().Where(o => o.OrderNumber == orderNumber).ToListAsync();
            return orderDetails.Select(o => new OrderDetailsDTO
            {
                OrderNumber = o.OrderNumber,
                PizzaId = o.PizzaId,
                PizzaName = o.Pizza.Name,  // Assuming there's a relationship between OrderDetails and Pizza
                Quantity = o.Quantity,
                Price = o.Price
            });
        }
    }

}
