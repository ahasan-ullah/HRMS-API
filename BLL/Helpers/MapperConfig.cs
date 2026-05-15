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
            c.CreateMap<Department, DepartmentDTO>().ReverseMap();
            c.CreateMap<Salary, SalaryDTO>().ReverseMap();
            c.CreateMap<SalaryRevision, SalaryRevisionDTO>().ReverseMap();
            c.CreateMap<Deduction, DeductionDTO>().ReverseMap();
            c.CreateMap<Bonus, BonusDTO>().ReverseMap();
            c.CreateMap<Payroll, PayrollDTO>().ReverseMap();
            c.CreateMap<PayrollDetail, PayrollDetailDTO>().ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
