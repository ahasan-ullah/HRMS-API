using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDepartmentRepo:IRepo<Department,int,bool>
    {
        bool NameExists(string name, int? excludeDepartmentId = null);
    }
}
