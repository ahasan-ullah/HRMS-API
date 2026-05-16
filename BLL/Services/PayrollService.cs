using BLL.DTOs;
using BLL.Helpers;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PayrollService
    {
        public static List<PayrollDTO> GetAll()
        {
            var data = DataAccessFactory.PayrollDataAccess().Get();
            return MapperConfig.GetMapper().Map<List<PayrollDTO>>(data);
        }
        public static PayrollDTO Get(int id)
        {
            var data = DataAccessFactory.PayrollDataAccess().Get(id);
            if (data == null)
            {
                return null;
            }
            return MapperConfig.GetMapper().Map<PayrollDTO>(data);
        }

        public static List<PayrollDetailDTO> GetDetails(int payrollId)
        {
            var payroll = DataAccessFactory.PayrollDataAccess().Get(payrollId);
            if (payroll == null)
            {
                throw new Exception("Payroll not found");
            }
            var data = DataAccessFactory.PayrollDetailDataAccess().GetByPayroll(payrollId).ToList();
            return MapperConfig.GetMapper().Map<List<PayrollDetailDTO>>(data);
        }
        public static bool Generate(int month, int year, int generatedBy)
        {
            if (month < 1 || month > 12)
                throw new Exception("Invalid month.");

            if (year < 1900 || year > 2100)
                throw new Exception("Invalid year.");

            if (DataAccessFactory.PayrollDataAccess().ExistsForPeriod(month, year))
                throw new Exception($"Payroll already generated for {month}/{year}.");

            var generatedByUser = DataAccessFactory.UserDataAccess().Get(generatedBy);
            if (generatedByUser == null)
                throw new Exception("Generating user not found.");

            var employees = DataAccessFactory.EmployeeDataAccess().Get();
            if (employees == null || employees.Count == 0)
                throw new Exception("No active employees found.");

            var payroll = new Payroll
            {
                PayrollMonth = month,
                PayrollYear = year,
                GeneratedDate = DateTime.Now,
                GeneratedBy = generatedBy,
                Status = "Draft",
                TotalAmount = 0
            };
            DataAccessFactory.PayrollDataAccess().Create(payroll);

            var savedPayroll = DataAccessFactory.PayrollDataAccess().GetByMonthYear(month, year);
            decimal totalAmount = 0;

            foreach (var employee in employees)
            {
                var activeSalary = DataAccessFactory.SalaryDataAccess().GetActiveSalaryByEmployeeId(employee.EmployeeId);
                if (activeSalary == null) continue;

                decimal bonusAmount = DataAccessFactory.BonusDataAccess()
                    .GetBonusesByEmployeeId(employee.EmployeeId)
                    .Where(b => b.BonusDate.Month == month && b.BonusDate.Year == year)
                    .Sum(b => b.Amount);

                decimal totalDeductions = DataAccessFactory.DeductionDataAccess()
                    .GetDeductionsByEmployeeIdAndMonthYear(employee.EmployeeId, month, year)
                    .Sum(d => d.Amount);


                decimal basicSalary = activeSalary.BasicSalary;
                decimal totalAllowances = activeSalary.HouseAllowance
                                        + activeSalary.MedicalAllowance
                                        + activeSalary.TransportAllowance;
                decimal grossSalary = basicSalary + totalAllowances + bonusAmount;
                decimal taxAmount = CalculateTax(grossSalary);
                decimal netSalary = grossSalary - totalDeductions - taxAmount;

                var detail = new PayrollDetail
                {
                    PayrollId = savedPayroll.PayrollId,
                    EmployeeId = employee.EmployeeId,
                    BasicSalary = basicSalary,
                    TotalAllowances = totalAllowances,
                    BonusAmount = bonusAmount,
                    TotalDeductions = totalDeductions,
                    TaxAmount = taxAmount,
                    NetSalary = netSalary,
                    PaymentStatus = "Pending"
                };

                DataAccessFactory.PayrollDetailDataAccess().Create(detail);
                totalAmount += netSalary;
            }
            savedPayroll.TotalAmount = totalAmount;
            return DataAccessFactory.PayrollDataAccess().Update(savedPayroll);
        }

        public static bool Approve(int payrollId)
        {
            var payroll = DataAccessFactory.PayrollDataAccess().Get(payrollId);
            if (payroll == null)
            {
                throw new Exception("Payroll not found");
            }
            if (payroll.Status != "Draft")
            {
                throw new Exception("Only draft payroll can be approved");
            }
            payroll.Status = "Approved";
            return DataAccessFactory.PayrollDataAccess().Update(payroll);
        }

        public static bool MarkAsPaid(int payrollId)
        {
            var payroll = DataAccessFactory.PayrollDataAccess().Get(payrollId);
            if (payroll == null)
            {
                throw new Exception("Payroll not found");
            }
            if (payroll.Status != "Approved")
            {
                throw new Exception("Only approved payroll can be marked as paid");
            }
            var details = DataAccessFactory.PayrollDetailDataAccess().GetByPayroll(payrollId);
            foreach (var detail in details)
            {
                detail.PaymentStatus = "Paid";
                DataAccessFactory.PayrollDetailDataAccess().Update(detail);
            }
            payroll.Status = "Paid";
            return DataAccessFactory.PayrollDataAccess().Update(payroll);
        }

        public static bool Delete(int id)
        {
            var payroll = DataAccessFactory.PayrollDataAccess().Get(id);
            if (payroll == null)
            {
                throw new Exception("Payroll not found");
            }
            if (payroll.Status != "Draft")
            {
                throw new Exception("Only draft payroll can be deleted");
            }
            var details = DataAccessFactory.PayrollDetailDataAccess().GetByPayroll(id);
            foreach (var detail in details)
            {
                DataAccessFactory.PayrollDetailDataAccess().Delete(detail.PayrollDetailId);
            }
            return DataAccessFactory.PayrollDataAccess().Delete(id);
        }

        private static decimal CalculateTax(decimal grossSalary)
        {
            if (grossSalary <= 20000) return 0;
            if (grossSalary <= 40000) return grossSalary * 0.05m;
            if (grossSalary <= 80000) return grossSalary * 0.10m;
            return grossSalary * 0.15m;
        }
    }
}
