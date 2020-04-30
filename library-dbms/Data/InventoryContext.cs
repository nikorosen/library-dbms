using System;
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
        public virtual DbSet<AssetCategory> AssetCategory { get; set; }
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
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SuppliedBy> SuppliedBy { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SLAPTOP\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True");
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
                    .HasMaxLength(12);

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(50);

               entity.Property(e => e.Manufacturer)
                    .HasColumnName("manufacturer")
                    .HasMaxLength(50);

                entity.Property(e => e.ModelNum)
                    .HasColumnName("model_num")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(255);

                entity.Property(e => e.SerialNum)
                    .HasColumnName("serial_num")
                    .HasMaxLength(50);

                entity.Property(e => e.AssetStatus)
                    .HasColumnName("asset_status")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AssetCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryID)
                    .HasName("asset_category$PrimaryKey");

                entity.ToTable("asset_category");

                entity.Property(e => e.CategoryID).HasColumnName("category_id");

                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<AssetLocation>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("asset_location$Index_F196FE77_CD58_49B2");

                entity.ToTable("asset_location");

                entity.HasIndex(e => e.AssetId)
                    .HasName("asset_location$Rel_3DFF26E2_E4F6_4FD4");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("building_num")
                    .HasMaxLength(4);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(255);

                entity.Property(e => e.RoomNum)
                    .HasColumnName("room_num")
                    .HasMaxLength(4);

                entity.HasOne(d => d.Asset)
                    .WithOne(p => p.AssetLocation)
                    .HasForeignKey<AssetLocation>(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("asset_location$Rel_3DFF26E2_E4F6_4FD4");
            });

            modelBuilder.Entity<AssignedToDep>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("assigned_to_dep$Index_E9BFA968_9E1B_44C8");

                entity.ToTable("assigned_to_dep");

                entity.HasIndex(e => e.AssetId)
                    .HasName("assigned_to_dep$Rel_64E24362_A635_469B");

                entity.HasIndex(e => e.DepartmentNum)
                    .HasName("assigned_to_dep$Rel_D5117F10_A9F9_4356");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.HasOne(d => d.Asset)
                    .WithOne(p => p.AssignedToDep)
                    .HasForeignKey<AssignedToDep>(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assigned_to_dep$Rel_64E24362_A635_469B");

                entity.HasOne(d => d.DepartmentNumNavigation)
                    .WithMany(p => p.AssignedToDep)
                    .HasForeignKey(d => d.DepartmentNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assigned_to_dep$Rel_D5117F10_A9F9_4356");
            });

            modelBuilder.Entity<AssignedToEmp>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("assigned_to_emp$Index_4143E321_EFA4_4B1B");

                entity.ToTable("assigned_to_emp");

                entity.HasIndex(e => e.AssetId)
                    .HasName("assigned_to_emp$Rel_9293AE47_A97C_44FE");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("assigned_to_emp$Rel_86004752_E8F8_4801");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.HasOne(d => d.Asset)
                    .WithOne(p => p.AssignedToEmp)
                    .HasForeignKey<AssignedToEmp>(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assigned_to_emp$Rel_9293AE47_A97C_44FE");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignedToEmp)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("assigned_to_emp$Rel_86004752_E8F8_4801");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentNum)
                    .HasName("department$Index_8E4A7CB3_8A33_47A1");

                entity.ToTable("department");

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<DepartmentLocation>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentNum, e.BuildingNum, e.RoomNum })
                    .HasName("department_location$Index_32791611_745F_46B1");

                entity.ToTable("department_location");

                entity.HasIndex(e => e.DepartmentNum)
                    .HasName("department_location$Rel_7E770B8A_9D8E_4B3C");

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("building_num")
                    .HasMaxLength(4);

                entity.Property(e => e.RoomNum)
                    .HasColumnName("room_num")
                    .HasMaxLength(4);

                entity.HasOne(d => d.DepartmentNumNavigation)
                    .WithMany(p => p.DepartmentLocation)
                    .HasForeignKey(d => d.DepartmentNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_location$Rel_7E770B8A_9D8E_4B3C");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.DepartmentNum)
                    .HasName("employee$Rel_11CE5EBA_BA9C_4D08");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.DepartmentNum).HasColumnName("department_num");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12);

                entity.HasOne(d => d.DepartmentNumNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentNum)
                    .HasConstraintName("employee$Rel_11CE5EBA_BA9C_4D08");
            });

            modelBuilder.Entity<EmployeeLocation>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.BuildingNum, e.RoomNum })
                    .HasName("employee_location$Index_8BC48910_8257_4055");

                entity.ToTable("employee_location");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("employee_location$Rel_5E0778F4_6BF9_4E3C");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("building_num")
                    .HasMaxLength(4);

                entity.Property(e => e.RoomNum)
                    .HasColumnName("room_num")
                    .HasMaxLength(4);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLocation)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_location$Rel_5E0778F4_6BF9_4E3C");
            });

            modelBuilder.Entity<LtsStaff>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("lts_staff$Index_EAEF5A50_4D82_4BDB");

                entity.ToTable("lts_staff");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("lts_staff$Rel_C2845076_B61A_423F");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LaborCost)
                    .HasColumnName("labor_cost")
                    .HasColumnType("money");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.LtsStaff)
                    .HasForeignKey<LtsStaff>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lts_staff$Rel_C2845076_B61A_423F");
            });

            modelBuilder.Entity<MaintenanceLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("maintenance_log$Index_817AC782_CA89_4215");

                entity.ToTable("maintenance_log");

                entity.HasIndex(e => e.AssetId)
                    .HasName("maintenance_log$Rel_B29742EB_3B1B_4EBC");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.AssetId).HasColumnName("asset_id");

                entity.Property(e => e.DatePerformed)
                    .HasColumnName("date_performed")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.HoursLogged).HasColumnName("hours_logged");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.MaintenanceLog)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_log$Rel_B29742EB_3B1B_4EBC");
            });

            modelBuilder.Entity<PerformedBy>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.LogId })
                    .HasName("performed_by$Index_F5E8F2D0_A1AB_4CC2");

                entity.ToTable("performed_by");

                entity.HasIndex(e => e.LogId)
                    .HasName("performed_by$Rel_2E859975_9CD4_4647");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.TotalCost)
                    .HasColumnName("total_cost")
                    .HasColumnType("money");

                entity.HasOne(d => d.Log)
                    .WithMany(p => p.PerformedBy)
                    .HasForeignKey(d => d.LogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("performed_by$Rel_2E859975_9CD4_4647");
            });

            modelBuilder.Entity<SalesRep>(entity =>
            {
                entity.HasKey(e => e.RepId)
                    .HasName("sales_rep$Index_F7ADC6BE_30BB_4E7A");

                entity.ToTable("sales_rep");

                entity.HasIndex(e => e.VendorId)
                    .HasName("sales_rep$Rel_BD8B7D81_23F8_4C13");

                entity.Property(e => e.RepId).HasColumnName("rep_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Ext)
                    .HasColumnName("ext")
                    .HasMaxLength(4);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.SalesRep)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sales_rep$Rel_BD8B7D81_23F8_4C13");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Status1)
                    .HasColumnName("Status")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SuppliedBy>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("supplied_by$Index_7D9EE7AC_5C9F_44CB");

                entity.ToTable("supplied_by");

                entity.HasIndex(e => e.VendorId)
                    .HasName("supplied_by$vendor_id");

                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchase_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnName("purchase_price")
                    .HasColumnType("money");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("company_name")
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasMaxLength(12);

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
