using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Context
{
    public class HRMSDbContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Salary> Salaries{ get; set; }
        public DbSet<SalaryRevision> SalaryRevisions { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PayrollDetail> PayrollDetails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Employee)
                .WithOptionalDependent(e => e.User)
                .Map(m => m.MapKey("EmployeeId"));

            modelBuilder.Entity<Salary>()
                .HasRequired(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalaryRevision>()
                .HasRequired(sr => sr.Employee)
                .WithMany(e => e.SalaryRevisions)
                .HasForeignKey(sr => sr.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalaryRevision>()
                .HasRequired(sr => sr.RevisedByUser)
                .WithMany(u => u.SalaryRevisions)
                .HasForeignKey(sr => sr.RevisedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Deduction>()
                .HasRequired(d => d.Employee)
                .WithMany(e => e.Deductions)
                .HasForeignKey(d => d.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bonus>()
                .HasRequired(b => b.Employee)
                .WithMany(e => e.Bonuses)
                .HasForeignKey(b => b.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payroll>()
                .HasRequired(p => p.GeneratedByUser)
                .WithMany(u => u.Payrolls)
                .HasForeignKey(p => p.GeneratedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PayrollDetail>()
                .HasRequired(pd => pd.Payroll)
                .WithMany(p => p.PayrollDetails)
                .HasForeignKey(pd => pd.PayrollId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PayrollDetail>()
                .HasRequired(pd => pd.Employee)
                .WithMany(e => e.PayrollDetails)
                .HasForeignKey(pd => pd.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RefreshToken>()
                .HasRequired(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
