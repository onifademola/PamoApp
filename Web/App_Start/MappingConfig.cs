﻿using Repo.DTOs;
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
                config.CreateMap<dto_Patients, patient>();

                config.CreateMap<AspNetUser, dto_AspNetUser>();
                config.CreateMap<dto_AspNetUser, AspNetUser>();

                config.CreateMap<biovital, dto_BioVital>();
                config.CreateMap<dto_BioVital, biovital>();

                config.CreateMap<attendance, dto_Attendance>();
                config.CreateMap<dto_Attendance, attendance>();

                config.CreateMap<prescription, dto_Prescription>();
                config.CreateMap<dto_Prescription, prescription>();

                config.CreateMap<labrequest, dto_LabRequest>();
                config.CreateMap<dto_LabRequest, labrequest>();

                config.CreateMap<Admission, dto_Admission>();
                config.CreateMap<dto_Admission, Admission>();

                config.CreateMap<ClinicWard, dto_ClinicWard>()
                .ForMember(dest => dest.UsedBeds, opt => opt.MapFrom(src => src.Admissions.Count(c => c.ClinicWardID.HasValue)));
                config.CreateMap<dto_ClinicWard, ClinicWard>();
                //.ReverseMap()
                //.ForMember(m => m.UsedBeds, m => m.Ignore())                
                //.ForMember(m => m.AvailableBeds, m => m.Ignore());
            });
        }
    }
}