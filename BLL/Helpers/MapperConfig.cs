using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class MapperConfig
    {
        static MapperConfiguration cfg= new MapperConfiguration(c =>
        {
            c.CreateMap<Employee, EmployeeDTO>().ReverseMap();

            c.CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees != null ? src.Employees.Count(e => e.IsActive) : 0)).ReverseMap();

            c.CreateMap<Salary, SalaryDTO>()
                .ForMember(dest => dest.GrossSalary, opt => opt.MapFrom(src =>src.BasicSalary + src.HouseAllowance + src.MedicalAllowance + src.TransportAllowance)).ReverseMap();

            c.CreateMap<SalaryRevision, SalaryRevisionDTO>().ReverseMap();
            c.CreateMap<Deduction, DeductionDTO>().ReverseMap();
            c.CreateMap<Bonus, BonusDTO>().ReverseMap();

            c.CreateMap<Payroll, PayrollDTO>()
                .ForMember(dest => dest.TotalEmployees, opt => opt.MapFrom(src =>  src.PayrollDetails != null ? src.PayrollDetails.Count : 0)).ReverseMap();

            c.CreateMap<PayrollDetail, PayrollDetailDTO>()
                .ForMember(dest => dest.GrossSalary, opt => opt.MapFrom(src => src.BasicSalary + src.TotalAllowances + src.BonusAmount)).ReverseMap();

            c.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee != null ? (int?)src.Employee.EmployeeId : null)).ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
