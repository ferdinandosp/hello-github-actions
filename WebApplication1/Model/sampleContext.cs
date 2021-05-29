using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Model
{
    public partial class sampleContext : DbContext
    {
        public sampleContext()
        {
        }

        public sampleContext(DbContextOptions<sampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Change> Changes { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<Dependency> Dependencies { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Release> Releases { get; set; }
        public virtual DbSet<SampleTable> SampleTables { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Vessel> Vessels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=sample;User Id=ferdinando;Password=12341234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "C.UTF-8");

            modelBuilder.Entity<Change>(entity =>
            {
                entity.ToTable("changes", "sqitch");

                entity.HasComment("Tracks the changes currently deployed to the database.");

                entity.HasIndex(e => new { e.Project, e.ScriptHash }, "changes_project_script_hash_key")
                    .IsUnique();

                entity.Property(e => e.ChangeId)
                    .HasColumnName("change_id")
                    .HasComment("Change primary key.");

                entity.Property(e => e.Change1)
                    .IsRequired()
                    .HasColumnName("change")
                    .HasComment("Name of a deployed change.");

                entity.Property(e => e.CommittedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("committed_at")
                    .HasDefaultValueSql("clock_timestamp()")
                    .HasComment("Date the change was deployed.");

                entity.Property(e => e.CommitterEmail)
                    .IsRequired()
                    .HasColumnName("committer_email")
                    .HasComment("Email address of the user who deployed the change.");

                entity.Property(e => e.CommitterName)
                    .IsRequired()
                    .HasColumnName("committer_name")
                    .HasComment("Name of the user who deployed the change.");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnName("note")
                    .HasDefaultValueSql("''::text")
                    .HasComment("Description of the change.");

                entity.Property(e => e.PlannedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("planned_at")
                    .HasComment("Date the change was added to the plan.");

                entity.Property(e => e.PlannerEmail)
                    .IsRequired()
                    .HasColumnName("planner_email")
                    .HasComment("Email address of the user who planned the change.");

                entity.Property(e => e.PlannerName)
                    .IsRequired()
                    .HasColumnName("planner_name")
                    .HasComment("Name of the user who planed the change.");

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasColumnName("project")
                    .HasComment("Name of the Sqitch project to which the change belongs.");

                entity.Property(e => e.ScriptHash)
                    .HasColumnName("script_hash")
                    .HasComment("Deploy script SHA-1 hash.");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Changes)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("changes_project_fkey");
            });

            modelBuilder.Entity<Crew>(entity =>
            {
                entity.ToTable("crews");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_on");

                entity.Property(e => e.LastUpdatedBy).HasColumnName("last_updated_by");

                entity.Property(e => e.LastUpdatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("last_updated_on");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.SeafarerId).HasColumnName("seafarer_id");
            });

            modelBuilder.Entity<Dependency>(entity =>
            {
                entity.HasKey(e => new { e.ChangeId, e.Dependency1 })
                    .HasName("dependencies_pkey");

                entity.ToTable("dependencies", "sqitch");

                entity.HasComment("Tracks the currently satisfied dependencies.");

                entity.Property(e => e.ChangeId)
                    .HasColumnName("change_id")
                    .HasComment("ID of the depending change.");

                entity.Property(e => e.Dependency1)
                    .HasColumnName("dependency")
                    .HasComment("Dependency name.");

                entity.Property(e => e.DependencyId)
                    .HasColumnName("dependency_id")
                    .HasComment("Change ID the dependency resolves to.");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasComment("Type of dependency.");

                entity.HasOne(d => d.Change)
                    .WithMany(p => p.DependencyChanges)
                    .HasForeignKey(d => d.ChangeId)
                    .HasConstraintName("dependencies_change_id_fkey");

                entity.HasOne(d => d.DependencyNavigation)
                    .WithMany(p => p.DependencyDependencyNavigations)
                    .HasForeignKey(d => d.DependencyId)
                    .HasConstraintName("dependencies_dependency_id_fkey");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => new { e.ChangeId, e.CommittedAt })
                    .HasName("events_pkey");

                entity.ToTable("events", "sqitch");

                entity.HasComment("Contains full history of all deployment events.");

                entity.Property(e => e.ChangeId)
                    .HasColumnName("change_id")
                    .HasComment("Change ID.");

                entity.Property(e => e.CommittedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("committed_at")
                    .HasDefaultValueSql("clock_timestamp()")
                    .HasComment("Date the event was committed.");

                entity.Property(e => e.Change)
                    .IsRequired()
                    .HasColumnName("change")
                    .HasComment("Change name.");

                entity.Property(e => e.CommitterEmail)
                    .IsRequired()
                    .HasColumnName("committer_email")
                    .HasComment("Email address of the user who committed the event.");

                entity.Property(e => e.CommitterName)
                    .IsRequired()
                    .HasColumnName("committer_name")
                    .HasComment("Name of the user who committed the event.");

                entity.Property(e => e.Conflicts)
                    .IsRequired()
                    .HasColumnName("conflicts")
                    .HasDefaultValueSql("'{}'::text[]")
                    .HasComment("Array of the names of conflicting changes.");

                entity.Property(e => e.Event1)
                    .IsRequired()
                    .HasColumnName("event")
                    .HasComment("Type of event.");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnName("note")
                    .HasDefaultValueSql("''::text")
                    .HasComment("Description of the change.");

                entity.Property(e => e.PlannedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("planned_at")
                    .HasComment("Date the event was added to the plan.");

                entity.Property(e => e.PlannerEmail)
                    .IsRequired()
                    .HasColumnName("planner_email")
                    .HasComment("Email address of the user who plan planned the change.");

                entity.Property(e => e.PlannerName)
                    .IsRequired()
                    .HasColumnName("planner_name")
                    .HasComment("Name of the user who planed the change.");

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasColumnName("project")
                    .HasComment("Name of the Sqitch project to which the change belongs.");

                entity.Property(e => e.Requires)
                    .IsRequired()
                    .HasColumnName("requires")
                    .HasDefaultValueSql("'{}'::text[]")
                    .HasComment("Array of the names of required changes.");

                entity.Property(e => e.Tags)
                    .IsRequired()
                    .HasColumnName("tags")
                    .HasDefaultValueSql("'{}'::text[]")
                    .HasComment("Tags associated with the change.");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("events_project_fkey");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Project1)
                    .HasName("projects_pkey");

                entity.ToTable("projects", "sqitch");

                entity.HasComment("Sqitch projects deployed to this database.");

                entity.HasIndex(e => e.Uri, "projects_uri_key")
                    .IsUnique();

                entity.Property(e => e.Project1)
                    .HasColumnName("project")
                    .HasComment("Unique Name of a project.");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("clock_timestamp()")
                    .HasComment("Date the project was added to the database.");

                entity.Property(e => e.CreatorEmail)
                    .IsRequired()
                    .HasColumnName("creator_email")
                    .HasComment("Email address of the user who added the project.");

                entity.Property(e => e.CreatorName)
                    .IsRequired()
                    .HasColumnName("creator_name")
                    .HasComment("Name of the user who added the project.");

                entity.Property(e => e.Uri)
                    .HasColumnName("uri")
                    .HasComment("Optional project URI");
            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("releases_pkey");

                entity.ToTable("releases", "sqitch");

                entity.HasComment("Sqitch registry releases.");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasComment("Version of the Sqitch registry.");

                entity.Property(e => e.InstalledAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("installed_at")
                    .HasDefaultValueSql("clock_timestamp()")
                    .HasComment("Date the registry release was installed.");

                entity.Property(e => e.InstallerEmail)
                    .IsRequired()
                    .HasColumnName("installer_email")
                    .HasComment("Email address of the user who installed the registry release.");

                entity.Property(e => e.InstallerName)
                    .IsRequired()
                    .HasColumnName("installer_name")
                    .HasComment("Name of the user who installed the registry release.");
            });

            modelBuilder.Entity<SampleTable>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("sample_table_pkey");

                entity.ToTable("sample_table");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Text).HasColumnName("text");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags", "sqitch");

                entity.HasComment("Tracks the tags currently applied to the database.");

                entity.HasIndex(e => new { e.Project, e.Tag1 }, "tags_project_tag_key")
                    .IsUnique();

                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .HasComment("Tag primary key.");

                entity.Property(e => e.ChangeId)
                    .IsRequired()
                    .HasColumnName("change_id")
                    .HasComment("ID of last change deployed before the tag was applied.");

                entity.Property(e => e.CommittedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("committed_at")
                    .HasDefaultValueSql("clock_timestamp()")
                    .HasComment("Date the tag was applied to the database.");

                entity.Property(e => e.CommitterEmail)
                    .IsRequired()
                    .HasColumnName("committer_email")
                    .HasComment("Email address of the user who applied the tag.");

                entity.Property(e => e.CommitterName)
                    .IsRequired()
                    .HasColumnName("committer_name")
                    .HasComment("Name of the user who applied the tag.");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnName("note")
                    .HasDefaultValueSql("''::text")
                    .HasComment("Description of the tag.");

                entity.Property(e => e.PlannedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("planned_at")
                    .HasComment("Date the tag was added to the plan.");

                entity.Property(e => e.PlannerEmail)
                    .IsRequired()
                    .HasColumnName("planner_email")
                    .HasComment("Email address of the user who planned the tag.");

                entity.Property(e => e.PlannerName)
                    .IsRequired()
                    .HasColumnName("planner_name")
                    .HasComment("Name of the user who planed the tag.");

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasColumnName("project")
                    .HasComment("Name of the Sqitch project to which the tag belongs.");

                entity.Property(e => e.Tag1)
                    .IsRequired()
                    .HasColumnName("tag")
                    .HasComment("Project-unique tag name.");

                entity.HasOne(d => d.Change)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ChangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tags_change_id_fkey");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tags_project_fkey");
            });

            modelBuilder.Entity<Vessel>(entity =>
            {
                entity.ToTable("vessels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_on");

                entity.Property(e => e.LastUpdatedBy).HasColumnName("last_updated_by");

                entity.Property(e => e.LastUpdatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("last_updated_on");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
