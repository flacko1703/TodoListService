﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TodoListProj.Infrastructure.EF.Contexts;

#nullable disable

namespace TodoListProj.Infrastructure.EF.Migrations
{
    [DbContext(typeof(TodoListReadDbContext))]
    partial class TodoListReadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.Entities.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean")
                        .HasColumnOrder(4);

                    b.Property<string>("Text")
                        .HasColumnType("text")
                        .HasColumnOrder(3);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.Property<Guid>("TodoListId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TodoListId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.TodoList", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Created")
                        .HasColumnOrder(3);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Updated")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.ToTable("TodoList");
                });

            modelBuilder.Entity("TodoListProj.Infrastructure.EF.Models.NoteModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean")
                        .HasColumnOrder(4);

                    b.Property<string>("Text")
                        .HasColumnType("text")
                        .HasColumnOrder(3);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.Property<Guid>("TodoListModelId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TodoListModelId");

                    b.ToTable("Notes", (string)null);
                });

            modelBuilder.Entity("TodoListProj.Infrastructure.EF.Models.TagModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.Property<Guid>("NoteModelId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NoteModelId");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("TodoListProj.Infrastructure.EF.Models.TodoListModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Created")
                        .HasColumnOrder(3);

                    b.Property<List<Guid>>("Notes")
                        .HasColumnType("uuid[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Updated")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.ToTable("TodoLists", (string)null);
                });

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.Entities.Note", b =>
                {
                    b.HasOne("TodoListProj.Domain.Aggregates.TodoListAggregate.TodoList", null)
                        .WithMany("Notes")
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.Entities.Tag", b =>
                {
                    b.HasOne("TodoListProj.Domain.Aggregates.TodoListAggregate.Entities.Note", null)
                        .WithMany("Tags")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoListProj.Infrastructure.EF.Models.NoteModel", b =>
                {
                    b.HasOne("TodoListProj.Infrastructure.EF.Models.TodoListModel", null)
                        .WithMany()
                        .HasForeignKey("TodoListModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoListProj.Infrastructure.EF.Models.TagModel", b =>
                {
                    b.HasOne("TodoListProj.Infrastructure.EF.Models.NoteModel", null)
                        .WithMany("Tags")
                        .HasForeignKey("NoteModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.Entities.Note", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("TodoListProj.Domain.Aggregates.TodoListAggregate.TodoList", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("TodoListProj.Infrastructure.EF.Models.NoteModel", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
