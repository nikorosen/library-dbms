using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace library_dbms.Models
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
        {
        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<AssetLocation> AssetLocation { get; set; }
        public virtual DbSet<AssignedToDep> AssignedToDep { get; set; }
        public virtual DbSet<AssignedToEmp> AssignedToEmp { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentLocation> DepartmentLocation { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeLocation> EmployeeLocation { get; set; }
        public virtual DbSet<LtsStaff> LtsStaff { get; set; }
        public virtual DbSet<MaintenanceLog> MaintenanceLog { get; set; }
        public virtual DbSet<PerformedBy> PerformedBy { get; set; }
        public virtual DbSet<SalesRep> SalesRep { get; set; }
        public virtual DbSet<SuppliedBy> SuppliedBy { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                
                // derive this from appsettings later
                string connString = "Data Source=SLAPTOP\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True"; //Configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("asset");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.BarcodeNum)
                    .HasColumnName("barcode_num")
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Manufacturer)
                    .HasColumnName("manufacturer")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModelNum)
                    .HasColumnName("model_num")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.SerialNum)
                    .HasColumnName("serial_num")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<AssetLocation>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PK__asset_lo__D28B561D931E5BFE");

                entity.ToTable("asset_location");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("building_num")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.RoomNum)
                    .HasColumnName("room_num")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Asset)
                    .WithOne(p => p.AssetLocation)
                    .HasForeignKey<AssetLocation>(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__asset_loc__asset__267ABA7A");
            });

            modelBuilder.Entity<AssignedToDep>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PK__assigned__D28B561D9A893273");

                entity.ToTable("assigned_to_dep");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.HasOne(d => d.Asset)
                    .WithOne(p => p.AssignedToDep)
                    .HasForeignKey<AssignedToDep>(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__assigned___asset__4F7CD00D");

                entity.HasOne(d => d.DepartmentNumNavigation)
                    .WithMany(p => p.AssignedToDep)
                    .HasForeignKey(d => d.DepartmentNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__assigned___depar__5070F446");
            });

            modelBuilder.Entity<AssignedToEmp>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PK__assigned__D28B561DE5033448");

                entity.ToTable("assigned_to_emp");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.HasOne(d => d.Asset)
                    .WithOne(p => p.AssignedToEmp)
                    .HasForeignKey<AssignedToEmp>(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__assigned___asset__4BAC3F29");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignedToEmp)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__assigned___emplo__4CA06362");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentNum)
                    .HasName("PK__departme__4F6C0AC595D7F8BE");

                entity.ToTable("department");

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<DepartmentLocation>(entity =>
            {
                entity.HasKey(e => e.DepartmentNum)
                    .HasName("PK__departme__4F6C0AC57DA7032D");

                entity.ToTable("department_location");

                entity.Property(e => e.DepartmentNum)
                    .HasColumnName("department_num")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("building_num")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RoomNum)
                    .HasColumnName("room_num")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.DepartmentNumNavigation)
                    .WithOne(p => p.DepartmentLocation)
                    .HasForeignKey<DepartmentLocation>(d => d.DepartmentNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__departmen__depar__2B3F6F97");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.DepartmentNumNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employee__depart__2E1BDC42");
            });

            modelBuilder.Entity<EmployeeLocation>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__employee__C52E0BA8577B2D45");

                entity.ToTable("employee_location");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuildingNum)
                    .IsRequired()
                    .HasColumnName("building_num")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RoomNum)
                    .IsRequired()
                    .HasColumnName("room_num")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.EmployeeLocation)
                    .HasForeignKey<EmployeeLocation>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employee___emplo__30F848ED");
            });

            modelBuilder.Entity<LtsStaff>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__lts_staf__C52E0BA8CF24AB39");

                entity.ToTable("lts_staff");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LaborCost)
                    .HasColumnName("labor_cost")
                    .HasDefaultValueSql("((20.00))");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.LtsStaff)
                    .HasForeignKey<LtsStaff>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__lts_staff__emplo__3C69FB99");
            });

            modelBuilder.Entity<MaintenanceLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__maintena__9E2397E00769E3FA");

                entity.ToTable("maintenance_log");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.DatePerformed)
                    .HasColumnName("date_performed")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.HoursLogged)
                    .HasColumnName("hours_logged")
                    .HasDefaultValueSql("((0.00))");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.MaintenanceLog)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__maintenan__asset__35BCFE0A");
            });

            modelBuilder.Entity<PerformedBy>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.LogId })
                    .HasName("PK__performe__DCCC32D677651DBC");

                entity.ToTable("performed_by");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.TotalCost).HasColumnName("total_cost");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PerformedBy)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__performed__emplo__440B1D61");

                entity.HasOne(d => d.Log)
                    .WithMany(p => p.PerformedBy)
                    .HasForeignKey(d => d.LogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__performed__log_i__44FF419A");
            });

            modelBuilder.Entity<SalesRep>(entity =>
            {
                entity.HasKey(e => e.RepId)
                    .HasName("PK__sales_re__DC905AF4B3106D95");

                entity.ToTable("sales_rep");

                entity.Property(e => e.RepId).HasColumnName("rep_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.SalesRep)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__sales_rep__vendo__412EB0B6");
            });

            modelBuilder.Entity<SuppliedBy>(entity =>
            {
                entity.HasKey(e => new { e.VendorId, e.AssetId })
                    .HasName("PK__supplied__C2559E19F0666CFA");

                entity.ToTable("supplied_by");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchase_date")
                    .HasColumnType("date");

                entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.SuppliedBy)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__supplied___asset__48CFD27E");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.SuppliedBy)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__supplied___vendo__47DBAE45");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("text");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("company_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
