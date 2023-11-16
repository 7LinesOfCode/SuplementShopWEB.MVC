using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Order;
using SuplementShopWEB.MVC.Domain.Interface;
using SuplementShopWEB.MVC.Domain.Models;

namespace SuplementShopWEB.MVC.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IItemRepository _itemRepo;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepo;
        private readonly IPdfGeneratorService _pdfGenerator;
        public OrderService(IOrderRepository repo, IMapper mapper, IItemRepository itemRepo, ICustomerRepository customerRepo, IPdfGeneratorService pdfGenerator)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
            _repo = repo;
            _itemRepo = itemRepo;
            _pdfGenerator = pdfGenerator;
        }

        public void CancelOrder(int orderId)
        {
            var OrderToCancel =  _repo.GetOrderById(orderId);
            if(!OrderToCancel.isDone)
            {
                OrderToCancel.isDone = true;
            }
        }

        public int CreateNewOrder(NewOrderVm newOrderVm)
        {
            if(newOrderVm is null)
            {
                return 0;
            }
            ///validacje null'a
            var newOrder = _mapper.Map<Order>(newOrderVm);

            //maping na Domain.Model.Order 

            var Item = _itemRepo.GetItemById(newOrder.ItemId);
            ///pobranie itemu z orderu
            var Customer = _customerRepo.GetCustomerById(newOrderVm.CustomerId);
            //pobranie customera z orderu

            newOrder.Price = Item.Price* newOrder.CountOfItems;
            /// ustalenie price order

            var result = _repo.UpdateItemCount(newOrder.ItemId, newOrder.CountOfItems);
            if (result == 0)
            {
                return result;
            }
            // update wartosci magaznowej

            var id = _repo.AddNewOrder(newOrder);

            ///Dodanie zamowienia


            //Dane do pdf'a

            var pdfData = PdfDataConvert(Item, newOrder, Customer);

            _pdfGenerator.CreatingPDF(pdfData);

            return id;
        }

        public OrderForListVm PdfDataConvert(Item Item, Order newOrder, Customer Customer)
        {
            var pdfData = new OrderForListVm();
            pdfData.Id = newOrder.Id;
            pdfData.ItemId = newOrder.ItemId;
            pdfData.Price = newOrder.Price;


            pdfData.CustomerFullName = Customer.FirstName + " " + Customer.LastName;

            pdfData.CountOfItems = newOrder.CountOfItems;

            var item = _itemRepo.GetItemById(newOrder.ItemId);
            pdfData.ItemName = item.Name;

            return pdfData;
        }


        public ListOrderForListVm GetActiveOrders(int pageSize, int pageNo, string searchString)
        {
            var activeOrders =  _repo.GetAllOrders()
                .Where(o=>o.isDone == false)
                .ProjectTo<OrderForListVm>(_mapper.ConfigurationProvider)
                .Where(c=>(c.CustomerFullName).Contains(searchString))
                .ToList();

            var ordersToShow = activeOrders.Skip(pageSize *(pageNo -1)).Take(pageSize).ToList();

            var resultVm = new ListOrderForListVm()
            {
                PageSize = pageSize,
                CurrentlyPage = pageNo,
                SearchString = searchString,
                Orders = ordersToShow,
                Count = activeOrders.Count
            };
            return resultVm;

        }

        public ListOrderForListVm GetAllOrders()
        {
            var orders = _repo.GetAllOrders();
            var resultVm = new ListOrderForListVm();
            resultVm.Orders = new List<OrderForListVm>();
            foreach (var order in orders)
            {
                var singleOrderVm = new OrderForListVm();
                singleOrderVm.Id = order.Id;
                singleOrderVm.CustomerFullName = order.Customer.FirstName + " " + order.Customer.LastName;
                singleOrderVm.CustomerId = order.Customer.Id;
                singleOrderVm.PhoneNumber = order.Customer.PhoneNumber;
                singleOrderVm.ItemId= order.ItemId;
                singleOrderVm.ItemName = order.Item.Name;
                singleOrderVm.Price = order.Price;

                resultVm.Orders.Add(singleOrderVm);
            }
            return resultVm;
        }
    }
}
