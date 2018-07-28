using AutoMapper;
using ProductShop.Models;
using ProductShopDatabase.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShopDatabase
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

        }
    }
}
