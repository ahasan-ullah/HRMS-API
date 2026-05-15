using DAL.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmployeeRepo:IRepo<Employee,int,bool>
    {
        bool EmailExists(string email, int? excludeEmployeeId = null);
        bool AccountNumberExists(string accountNumber, int? excludeEmployeeId = null);
        Employee GetByEmail(string email);
    }
}
