using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    public class HRMSEnums
    {
        public enum EmploymentStatus
        {
            Active,
            Inactive,
            Terminated,
            OnLeave
        }
        public enum PayrollStatus
        {
            Draft,
            Approved,
            Paid
        }
        public enum PaymentStatus
        {
            Pending,
            Paid
        }
        public enum UserRole
        {
            Admin,
            HR,
            Employee
        }
        public enum DeductionType
        {
            Absent,
            Loan,
            Penalty,
            Tax,
            Other
        }
        public enum BonusType
        {
            Festival,
            Performance,
            Annual,
            Other
        }
        public enum SalaryStatus
        {
            Active,
            Inactive
        }
    }
}
