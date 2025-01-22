﻿// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Herramienta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Herramienta");
                });

            modelBuilder.Entity("Data.Models.Instalacion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Capacidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraFin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraInicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Instalacion");
                });

            modelBuilder.Entity("Data.Models.Reporte", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("RecursoAfectado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Reportes");
                });

            modelBuilder.Entity("Data.Models.ReservacionHerramienta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("Herramienta_ID")
                        .HasColumnType("int");

                    b.Property<string>("HoraFin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraInicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Usuario_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Herramienta_ID");

                    b.HasIndex("Usuario_ID");

                    b.ToTable("ReservasHerramientas");
                });

            modelBuilder.Entity("Data.Models.ReservacionInstalacion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("Instalacion_ID")
                        .HasColumnType("int");

                    b.Property<int>("Usuario_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Instalacion_ID");

                    b.HasIndex("Usuario_ID");

                    b.ToTable("ReservasInstalaciones");
                });

            modelBuilder.Entity("Data.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Data.Models.ReservacionHerramienta", b =>
                {
                    b.HasOne("Data.Models.Herramienta", "Herramienta")
                        .WithMany("ReservacionHerramientas")
                        .HasForeignKey("Herramienta_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Data.Models.Usuario", "Usuario")
                        .WithMany("ReservacionHerramientas")
                        .HasForeignKey("Usuario_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Herramienta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Data.Models.ReservacionInstalacion", b =>
                {
                    b.HasOne("Data.Models.Instalacion", "Instalacion")
                        .WithMany("ReservacionInstalaciones")
                        .HasForeignKey("Instalacion_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Data.Models.Usuario", "Usuario")
                        .WithMany("ReservacionInstalaciones")
                        .HasForeignKey("Usuario_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Instalacion");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Data.Models.Herramienta", b =>
                {
                    b.Navigation("ReservacionHerramientas");
                });

            modelBuilder.Entity("Data.Models.Instalacion", b =>
                {
                    b.Navigation("ReservacionInstalaciones");
                });

            modelBuilder.Entity("Data.Models.Usuario", b =>
                {
                    b.Navigation("ReservacionHerramientas");

                    b.Navigation("ReservacionInstalaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
