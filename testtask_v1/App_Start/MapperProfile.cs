using AutoMapper;
using BLL.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtask_v1.App_Start
{
    public class MapperProfile : Profile
    {
        
        public MapperProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}