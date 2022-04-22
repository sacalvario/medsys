
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ECN.Models
{
    public partial class EcnContext : DbContext
    {
        public EcnContext()
        {
        }

        public EcnContext(DbContextOptions<EcnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Changetype> Changetypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Documenttype> Documenttypes { get; set; }
        public virtual DbSet<Ecn> Ecns { get; set; }
        public virtual DbSet<EcnAttachment> EcnAttachments { get; set; }
        public virtual DbSet<EcnDocumenttype> EcnDocumenttypes { get; set; }
        public virtual DbSet<EcnEco> EcnEcos { get; set; }
        public virtual DbSet<EcnNumberpart> EcnNumberparts { get; set; }
        public virtual DbSet<EcnRevision> EcnRevisions { get; set; }
        public virtual DbSet<EcoType> EcoTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Numberpart> Numberparts { get; set; }
        public virtual DbSet<NumberpartType> NumberpartTypes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                _ = optionsBuilder.UseMySql("server=192.168.36.4;user id=usermysql;password=user;database=ecn", ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("attachment");

                entity.HasIndex(e => e.AttachmentId, "Attachment_ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AttachmentId).HasColumnName("Attachment_ID");

                entity.Property(e => e.AttachmentFile)
                    .IsRequired()
                    .HasColumnName("Attachment_FIle");

                entity.Property(e => e.AttachmentFilename)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("Attachment_Filename");
            });

            modelBuilder.Entity<Changetype>(entity =>
            {
                entity.ToTable("changetype");

                entity.HasIndex(e => e.ChangeTypeId, "ChangeType_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ChangeTypeName, "ChangeType_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeType_ID");

                entity.Property(e => e.ChangeTypeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("ChangeType_Name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.CustomerId, "Customer_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerName, "Customer_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Customer_Name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.DepartmentId, "Department_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.DepartmentName, "Department_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Department_Name");
            });

            modelBuilder.Entity<Documenttype>(entity =>
            {
                entity.ToTable("documenttype");

                entity.HasIndex(e => e.DocumentTypeId, "DocumentType_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.DocumentTypeName, "DocumentType_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_ID");

                entity.Property(e => e.DocumentTypeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("DocumentType_Name");
            });

            modelBuilder.Entity<Ecn>(entity =>
            {
                entity.ToTable("ecn");

                entity.HasIndex(e => e.Id, "ECN_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ChangeTypeId, "fk_changetype_id_idx");

                entity.HasIndex(e => e.DocumentTypeId, "fk_documenttype_id_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_employee_id_idx");

                entity.HasIndex(e => e.StatusId, "fk_status_id_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChangeDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("Change_Description");

                entity.Property(e => e.ChangeJustification)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("Change_Justification");

                entity.Property(e => e.ChangeTypeId).HasColumnName("ChangeType_ID");

                entity.Property(e => e.DocumentLvl)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Document_Lvl");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Document_Name");

                entity.Property(e => e.DocumentNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Document_No");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_ID");

                entity.Property(e => e.DrawingLvl)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Drawing_Lvl");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.IsEco).HasColumnName("Is_Eco");

                entity.Property(e => e.ManufacturingAffectations)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("Manufacturing_Affectations");

                entity.Property(e => e.Notes)
                   .HasMaxLength(1000)
                   .HasColumnName("Notes");

                entity.Property(e => e.OldDocumentLvl)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("OldDocument_Lvl");

                entity.Property(e => e.OldDrawingLvl)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("OldDrawing_Lvl");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.HasOne(d => d.ChangeType)
                    .WithMany(p => p.Ecns)
                    .HasForeignKey(d => d.ChangeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_changetype_id");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Ecns)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_documenttype_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Ecns)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Ecns)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_status_id");
            });

            modelBuilder.Entity<EcnAttachment>(entity =>
            {
                entity.HasKey(e => new { e.EcnId, e.AttachmentId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("ecn_attachments");

                entity.HasIndex(e => e.AttachmentId, "fk_id-attachment_idx");

                entity.Property(e => e.EcnId).HasColumnName("ECN_ID");

                entity.Property(e => e.AttachmentId).HasColumnName("Attachment_ID");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.EcnAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id-attachment");

                entity.HasOne(d => d.Ecn)
                    .WithMany(p => p.EcnAttachments)
                    .HasForeignKey(d => d.EcnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id-ecn");
            });

            modelBuilder.Entity<EcnDocumenttype>(entity =>
            {
                entity.HasKey(e => new { e.EcnId, e.DocumentTypeId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("ecn_documenttype");

                entity.HasIndex(e => e.DocumentTypeId, "fk-id-documenttype_idx");

                entity.Property(e => e.EcnId).HasColumnName("ECN_ID");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_ID");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.EcnDocumenttypes)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-id-documenttype");

                entity.HasOne(d => d.Ecn)
                    .WithMany(p => p.EcnDocumenttypes)
                    .HasForeignKey(d => d.EcnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-id-ecn");
            });

            modelBuilder.Entity<EcnEco>(entity =>
            {
                entity.HasKey(e => e.IdEcn)
                    .HasName("PRIMARY");

                entity.ToTable("ecn_eco");

                entity.HasIndex(e => e.IdEcn, "ID_Ecn_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdEco, "ID_Eco_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EcoTypeId, "fk_ecotype_id_idx");

                entity.Property(e => e.IdEcn)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Ecn");

                entity.Property(e => e.EcoTypeId).HasColumnName("EcoType_ID");

                entity.Property(e => e.IdEco)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("ID_Eco");

                entity.HasOne(d => d.EcoType)
                    .WithMany(p => p.EcnEcos)
                    .HasForeignKey(d => d.EcoTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ecotype_id");

                entity.HasOne(d => d.IdEcnNavigation)
                    .WithOne(p => p.EcnEco)
                    .HasForeignKey<EcnEco>(d => d.IdEcn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ecn_id");
            });

            modelBuilder.Entity<EcnNumberpart>(entity =>
            {
                entity.HasKey(e => new { e.EcnId, e.ProductId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("ecn_numberpart");

                entity.HasIndex(e => e.EcnId, "ecn_id_idx");

                entity.HasIndex(e => e.ProductId, "product_id_idx");

                entity.Property(e => e.EcnId).HasColumnName("ECN_ID");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.HasOne(d => d.Ecn)
                    .WithMany(p => p.EcnNumberparts)
                    .HasForeignKey(d => d.EcnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ecn_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.EcnNumberparts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_id");
            });

            modelBuilder.Entity<EcnRevision>(entity =>
            {
                entity.HasKey(e => new { e.RevisionSequence, e.EcnId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("ecn_revision");

                entity.HasIndex(e => e.EcnId, "ecn_id_idx");

                entity.HasIndex(e => e.EmployeeId, "employee_id_idx");

                entity.HasIndex(e => e.StatusId, "status_fk_id_idx");

                entity.Property(e => e.RevisionSequence).HasColumnName("Revision_Sequence");

                entity.Property(e => e.EcnId).HasColumnName("ECN_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.RevisionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Revision_Date");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.HasOne(d => d.Ecn)
                    .WithMany(p => p.EcnRevisions)
                    .HasForeignKey(d => d.EcnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ecn_id_fk");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EcnRevisions)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_fk_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.EcnRevisions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("status_fk_id");
            });

            modelBuilder.Entity<EcoType>(entity =>
            {
                entity.ToTable("eco_type");

                entity.HasIndex(e => e.EcoTypeId, "EcoType_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EcoTypeName, "EcoType_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.EcoTypeId).HasColumnName("EcoType_ID");

                entity.Property(e => e.EcoTypeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("EcoType_Name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.EmployeeId, "Employee_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.DepartmentId, "fk_department_id_idx");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Employee_ID");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Employee_Email");

                entity.Property(e => e.EmployeeFirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Employee_FirstName");

                entity.Property(e => e.EmployeeLastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Employee_LastName");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_department_id");
            });

            modelBuilder.Entity<Numberpart>(entity =>
            {
                entity.HasKey(e => e.NumberPartNo)
                    .HasName("PRIMARY");

                entity.ToTable("numberpart");

                entity.HasIndex(e => e.NumberPartId, "NumberPart_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NumberPartNo, "NumberPart_NO_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId, "fk_customer_id_idx");

                entity.HasIndex(e => e.NumberPartType, "fk_numberpart_type_idx");

                entity.Property(e => e.NumberPartNo).HasColumnName("NumberPart_NO");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.NumberPartId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("NumberPart_ID");

                entity.Property(e => e.NumberPartRev)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("NumberPart_Rev");

                entity.Property(e => e.NumberPartType).HasColumnName("NumberPart_Type");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Numberparts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_id");

                entity.HasOne(d => d.NumberPartTypeNavigation)
                    .WithMany(p => p.Numberparts)
                    .HasForeignKey(d => d.NumberPartType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_numberpart_type");
            });

            modelBuilder.Entity<NumberpartType>(entity =>
            {
                entity.ToTable("numberpart_type");

                entity.HasIndex(e => e.NumberPartTypeId, "NumberPart_Type_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NumberPartTypeName, "NumberPart_Type_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.NumberPartTypeId).HasColumnName("NumberPart_Type_ID");

                entity.Property(e => e.NumberPartTypeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("NumberPart_Type_Name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.HasIndex(e => e.StatusId, "Status_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StatusName, "Status_Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Status_Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                   .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasComment("		");

                entity.HasIndex(e => e.EmployeeId, "Employee_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "User_ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Username).HasMaxLength(45);

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_id_");
            });

            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.IsEcoVisibility);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.IsEcoToString);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.LongEndDate);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.CurrentSignature);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.SignatureCount);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.EmployeeName);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.StatusName);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.ChangeTypeName);
            _ = modelBuilder.Entity<Ecn>().Ignore(t => t.DocumentTypeName);
            _ = modelBuilder.Entity<Employee>().Ignore(t => t.Name);
            _ = modelBuilder.Entity<Employee>().Ignore(t => t.Index);
            _ = modelBuilder.Entity<Documenttype>().Ignore(t => t.IsSelected);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
