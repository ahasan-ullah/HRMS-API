namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bonus",
                c => new
                    {
                        BonusId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        BonusType = c.String(nullable: false, maxLength: 100),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BonusDate = c.DateTime(nullable: false),
                        Reason = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BonusId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 500),
                        Position = c.String(nullable: false, maxLength: 100),
                        AccountNumber = c.String(nullable: false, maxLength: 50),
                        EmploymentStatus = c.String(nullable: false, maxLength: 20),
                        JoiningDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Deductions",
                c => new
                    {
                        DeductionId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DeductionType = c.String(nullable: false, maxLength: 100),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Reason = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeductionId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.PayrollDetails",
                c => new
                    {
                        PayrollDetailId = c.Int(nullable: false, identity: true),
                        PayrollId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        BasicSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAllowances = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BonusAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDeductions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentStatus = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.PayrollDetailId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Payrolls", t => t.PayrollId)
                .Index(t => t.PayrollId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Payrolls",
                c => new
                    {
                        PayrollId = c.Int(nullable: false, identity: true),
                        PayrollMonth = c.Int(nullable: false),
                        PayrollYear = c.Int(nullable: false),
                        GeneratedDate = c.DateTime(nullable: false),
                        GeneratedBy = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.PayrollId)
                .ForeignKey("dbo.Users", t => t.GeneratedBy)
                .Index(t => t.GeneratedBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false, maxLength: 255),
                        Role = c.String(nullable: false, maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.SalaryRevisions",
                c => new
                    {
                        RevisionId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        OldSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NewSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RevisionDate = c.DateTime(nullable: false),
                        Reason = c.String(maxLength: 500),
                        RevisedBy = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RevisionId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Users", t => t.RevisedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.RevisedBy);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        SalaryId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        BasicSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HouseAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedicalAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransportAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffectiveDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Remarks = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SalaryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bonus", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Salaries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PayrollDetails", "PayrollId", "dbo.Payrolls");
            DropForeignKey("dbo.Payrolls", "GeneratedBy", "dbo.Users");
            DropForeignKey("dbo.SalaryRevisions", "RevisedBy", "dbo.Users");
            DropForeignKey("dbo.SalaryRevisions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Users", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PayrollDetails", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Deductions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Salaries", new[] { "EmployeeId" });
            DropIndex("dbo.SalaryRevisions", new[] { "RevisedBy" });
            DropIndex("dbo.SalaryRevisions", new[] { "EmployeeId" });
            DropIndex("dbo.Users", new[] { "EmployeeId" });
            DropIndex("dbo.Payrolls", new[] { "GeneratedBy" });
            DropIndex("dbo.PayrollDetails", new[] { "EmployeeId" });
            DropIndex("dbo.PayrollDetails", new[] { "PayrollId" });
            DropIndex("dbo.Deductions", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Bonus", new[] { "EmployeeId" });
            DropTable("dbo.Salaries");
            DropTable("dbo.SalaryRevisions");
            DropTable("dbo.Users");
            DropTable("dbo.Payrolls");
            DropTable("dbo.PayrollDetails");
            DropTable("dbo.Departments");
            DropTable("dbo.Deductions");
            DropTable("dbo.Employees");
            DropTable("dbo.Bonus");
        }
    }
}
