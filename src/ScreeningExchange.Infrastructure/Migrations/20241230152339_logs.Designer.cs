﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScreeningExchange.Infrastructure.DataAccess;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241230152339_logs")]
    partial class logs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("screeningexchange")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.AgentsAggregate.Agent", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "ScreeningExchange.Domain.Aggregates.AgentsAggregate.Agent.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "ScreeningExchange.Domain.Aggregates.AgentsAggregate.Agent.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "ScreeningExchange.Domain.Aggregates.AgentsAggregate.Agent.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Agent", "screeningexchange");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.DestinationsAggregate.Destination", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Awnser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("BuildQuestionId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeFinished")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("StudentId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuildQuestionId");

                    b.HasIndex("StudentId");

                    b.ToTable("Destination", "screeningexchange");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate.LinkDispatcher", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("BuildQuestionId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailSentSuccess")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<bool>("SendToEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("SendToWhatsApp")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("WasRead")
                        .HasColumnType("bit");

                    b.Property<bool>("WhatsappSentSuccess")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Customer", "ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate.LinkDispatcher.Customer#Customer", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("BuildQuestionId");

                    b.ToTable("LinkDispatcher", "screeningexchange");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.QuestionsAggregate.BuildQuestion", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Flows")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SendToEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("SendToWhatsApp")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("BuildQuestion", "screeningexchange");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.SchoolsAggregate.School", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "ScreeningExchange.Domain.Aggregates.SchoolsAggregate.School.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "ScreeningExchange.Domain.Aggregates.SchoolsAggregate.School.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "ScreeningExchange.Domain.Aggregates.SchoolsAggregate.School.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("School", "screeningexchange");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.StudentiesAggregate.Student", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "ScreeningExchange.Domain.Aggregates.StudentiesAggregate.Student.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "ScreeningExchange.Domain.Aggregates.StudentiesAggregate.Student.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "ScreeningExchange.Domain.Aggregates.StudentiesAggregate.Student.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Student", "screeningexchange");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.DestinationsAggregate.Destination", b =>
                {
                    b.HasOne("ScreeningExchange.Domain.Aggregates.QuestionsAggregate.BuildQuestion", "BuildQuestion")
                        .WithMany()
                        .HasForeignKey("BuildQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScreeningExchange.Domain.Aggregates.StudentiesAggregate.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildQuestion");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate.LinkDispatcher", b =>
                {
                    b.HasOne("ScreeningExchange.Domain.Aggregates.QuestionsAggregate.BuildQuestion", "BuildQuestion")
                        .WithMany()
                        .HasForeignKey("BuildQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("System.Collections.Generic.List<ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate.Log>", "Logs", b1 =>
                        {
                            b1.Property<byte[]>("LinkDispatcherId")
                                .HasColumnType("varbinary(16)");

                            b1.Property<int>("Capacity")
                                .HasColumnType("int");

                            b1.HasKey("LinkDispatcherId");

                            b1.ToTable("LinkDispatcher", "screeningexchange");

                            b1.ToJson("Logs");

                            b1.WithOwner()
                                .HasForeignKey("LinkDispatcherId");
                        });

                    b.Navigation("BuildQuestion");

                    b.Navigation("Logs")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
