using AutoMapper;
using Mc2.CrudTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Application.Services.Dto;

namespace Mc2.CrudTest.Application.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();

        }
    }
}
