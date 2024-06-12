using AutoMapper;
using DSI_Assignment.Application.DTO;
using DSI_Assignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
           CreateMap<Item, ItemDto>().ReverseMap();
           CreateMap<Supplier, SupplierDto>().ReverseMap();
           CreateMap<Order, OrderDto>().ReverseMap();
           CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        }
    }
}
