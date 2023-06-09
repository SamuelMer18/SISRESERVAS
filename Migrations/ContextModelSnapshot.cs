﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SISRESERVAS.Data;

#nullable disable

namespace SISRESERVAS.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SISRESERVAS.Models.bus", b =>
                {
                    b.Property<int>("idbus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idbus"));

                    b.Property<int>("nombrebus")
                        .HasColumnType("int");

                    b.HasKey("idbus");

                    b.ToTable("Viajes");
                });

            modelBuilder.Entity("SISRESERVAS.Models.chofer", b =>
                {
                    b.Property<int>("Idchofer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Idchofer"));

                    b.Property<string>("Nombrechofer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Idchofer");

                    b.ToTable("chofer");
                });

            modelBuilder.Entity("SISRESERVAS.Models.departamento", b =>
                {
                    b.Property<int>("IdDep")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDep"));

                    b.Property<string>("NombreDep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.HasKey("IdDep");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("SISRESERVAS.Models.reserva", b =>
                {
                    b.Property<int>("IdRes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRes"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnType("datetime2");

                    b.Property<int>("busidbus")
                        .HasColumnType("int");

                    b.Property<int>("choferIdchofer")
                        .HasColumnType("int");

                    b.Property<int>("departamentoIdDep")
                        .HasColumnType("int");

                    b.Property<int>("usuarioId")
                        .HasColumnType("int");

                    b.HasKey("IdRes");

                    b.HasIndex("busidbus");

                    b.HasIndex("choferIdchofer");

                    b.HasIndex("departamentoIdDep");

                    b.HasIndex("usuarioId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("SISRESERVAS.Models.usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SISRESERVAS.Models.reserva", b =>
                {
                    b.HasOne("SISRESERVAS.Models.bus", "bus")
                        .WithMany()
                        .HasForeignKey("busidbus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SISRESERVAS.Models.chofer", "chofer")
                        .WithMany()
                        .HasForeignKey("choferIdchofer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SISRESERVAS.Models.departamento", "departamento")
                        .WithMany()
                        .HasForeignKey("departamentoIdDep")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SISRESERVAS.Models.usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bus");

                    b.Navigation("chofer");

                    b.Navigation("departamento");

                    b.Navigation("usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
