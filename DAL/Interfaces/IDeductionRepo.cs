using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDeductionRepo:IRepo<Deduction,int,Deduction>
    {
        List<Deduction> GetDeductionsByEmployeeId(int employeeId);
        List<Deduction> GetDeductionsByEmployeeIdAndMonthYear(int employeeId, int month, int year);
        //List<Deduction> GetDeductionsByMonthYear(int month, int year);
        //bool HasDeductionForEmployeeInMonthYear(int employeeId, int month, int year);
        //decimal GetTotalDeductionsForEmployeeInMonthYear(int employeeId, int month, int year);
        //decimal GetTotalDeductionsForMonthYear(int month, int year);
        //decimal GetTotalDeductionsForEmployee(int employeeId);
        //void DeleteDeductionsByEmployeeIdAndMonthYear(int employeeId, int month, int year);
        //void DeleteDeductionsByMonthYear(int month, int year);
        //void DeleteDeductionsByEmployeeId(int employeeId);
        //void DeleteAllDeductions();
        //List<Deduction> SearchDeductions(string searchTerm);
        //List<Deduction> GetRecentDeductions(int count);
        //List<Deduction> GetDeductionsWithReason(string reason);
    }
}
