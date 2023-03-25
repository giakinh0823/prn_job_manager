﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prn_job_manager.Models
{
    public partial class CronJobContext : DbContext
    {
        public CronJobContext()
        {
        }

        public CronJobContext(DbContextOptions<CronJobContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobSchedule> JobSchedules { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserJob> UserJobs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HAGIAKINH;database=cron_job;Integrated security=true;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Expression)
                    .IsUnicode(false)
                    .HasColumnName("expression");

                entity.Property(e => e.Header)
                    .IsUnicode(false)
                    .HasColumnName("header");

                entity.Property(e => e.Method)
                    .IsUnicode(false)
                    .HasColumnName("method");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Payload)
                    .IsUnicode(false)
                    .HasColumnName("payload");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Webhook)
                    .IsUnicode(false)
                    .HasColumnName("webhook");
            });

            modelBuilder.Entity<JobSchedule>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Job_Schedule");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.Frequency).HasColumnName("frequency");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.LastRun)
                    .HasColumnType("datetime")
                    .HasColumnName("last_run");

                entity.Property(e => e.NextRun)
                    .HasColumnType("datetime")
                    .HasColumnName("next_run");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.Job)
                    .WithMany()
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Job_Sched__job_i__25869641");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.Output)
                    .HasColumnType("text")
                    .HasColumnName("output");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Log__job_id__2D27B809");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Log__user_id__2E1BDC42");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<UserJob>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("User_Job");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Job)
                    .WithMany()
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__User_Job__job_id__2A4B4B5E");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__User_Job__user_i__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
