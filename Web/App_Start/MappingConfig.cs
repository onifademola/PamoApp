using Repo.DTOs;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config => {

                //invoice mapping
                //config.CreateMap<InvoiceHeader, InvoiceHeaderVm>();
                // .ForMember(dest => dest.ArmDescription, opt => opt.MapFrom(v => _clrepo.ClassArmsForGrid().FirstOrDefault(n => n.ClassArmId == v.ArmID).ClassArm));
                //config.CreateMap<vwInvoice, vwInvoiceVm>();
                //config.CreateMap<InvoiceDetail, InvoiceDetailVm>();
                //config.CreateMap<InvoiceDetail, InvoiceDetailVmCompact>()
                //.ForMember(dest => dest.WhatToPay, opt => opt.MapFrom(src => src.Balance));

                //Patient to eregister mapping
                config.CreateMap<patient, dto_Patients>();
            });
        }
    }
}