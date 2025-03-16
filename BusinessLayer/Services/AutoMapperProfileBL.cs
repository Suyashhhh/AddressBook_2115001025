using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BusinessLayer.Services
{
    public class AutoMapperProfileBL : Profile
    {
        public AutoMapperProfileBL()
        {
            CreateMap<AddressBookEntity, ResponseAddressBookModel>().ReverseMap();
        }
    }
}