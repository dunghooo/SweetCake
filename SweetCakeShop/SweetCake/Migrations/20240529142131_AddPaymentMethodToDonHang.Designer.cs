﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SweetCake.Data;

#nullable disable

namespace SweetCake.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240529142131_AddPaymentMethodToDonHang")]
    partial class AddPaymentMethodToDonHang
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SweetCake.Models.Anh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SanphamId")
                        .HasColumnType("int");

                    b.Property<string>("TenAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SanphamId");

                    b.ToTable("ANH");
                });

            modelBuilder.Entity("SweetCake.Models.ChiTiet_SP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Gia")
                        .HasColumnType("int");

                    b.Property<DateTime>("HanSuDung")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgaySanXuat")
                        .HasColumnType("datetime2");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SanPhamId");

                    b.ToTable("CHI_TIET_SP");
                });

            modelBuilder.Entity("SweetCake.Models.DonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ThoiGianHuy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrangThaiDonHang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("TrangThaiThanhToan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("DON_HANG");
                });

            modelBuilder.Entity("SweetCake.Models.DonHang_ChiTiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChiTiet_SPId")
                        .HasColumnType("int");

                    b.Property<int>("DonHangId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChiTiet_SPId");

                    b.HasIndex("DonHangId");

                    b.ToTable("DONHANG_CHITIET");
                });

            modelBuilder.Entity("SweetCake.Models.LoaiSP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("LOAI_SP");
                });

            modelBuilder.Entity("SweetCake.Models.SanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LoaiSPId")
                        .HasColumnType("int");

                    b.Property<string>("Mota")
                        .HasColumnType("ntext");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LoaiSPId");

                    b.ToTable("SAN_PHAM");
                });

            modelBuilder.Entity("SweetCake.Models.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(30)");

                    b.Property<bool>("LoaiTK")
                        .HasColumnType("bit");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("Varchar(60)");

                    b.Property<DateTime>("NgayDangKy")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("Varchar(11)");

                    b.Property<string>("TenTK")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TAI_KHOAN");
                });

            modelBuilder.Entity("SweetCake.Models.ThongTin_NhanHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DonhangId")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("THONGTIN_NHANHANG");
                });

            modelBuilder.Entity("SweetCake.Models.Anh", b =>
                {
                    b.HasOne("SweetCake.Models.SanPham", "SanPham")
                        .WithMany("Anhs")
                        .HasForeignKey("SanphamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("SweetCake.Models.ChiTiet_SP", b =>
                {
                    b.HasOne("SweetCake.Models.SanPham", "SanPham")
                        .WithMany("ChiTietSPs")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("SweetCake.Models.DonHang", b =>
                {
                    b.HasOne("SweetCake.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("DonHangs")
                        .HasForeignKey("TaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("SweetCake.Models.DonHang_ChiTiet", b =>
                {
                    b.HasOne("SweetCake.Models.ChiTiet_SP", "ChiTiet_SP")
                        .WithMany()
                        .HasForeignKey("ChiTiet_SPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SweetCake.Models.DonHang", "DonHang")
                        .WithMany("DonHang_ChiTiets")
                        .HasForeignKey("DonHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTiet_SP");

                    b.Navigation("DonHang");
                });

            modelBuilder.Entity("SweetCake.Models.SanPham", b =>
                {
                    b.HasOne("SweetCake.Models.LoaiSP", "LoaiSP")
                        .WithMany("SanPhams")
                        .HasForeignKey("LoaiSPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiSP");
                });

            modelBuilder.Entity("SweetCake.Models.DonHang", b =>
                {
                    b.Navigation("DonHang_ChiTiets");
                });

            modelBuilder.Entity("SweetCake.Models.LoaiSP", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("SweetCake.Models.SanPham", b =>
                {
                    b.Navigation("Anhs");

                    b.Navigation("ChiTietSPs");
                });

            modelBuilder.Entity("SweetCake.Models.TaiKhoan", b =>
                {
                    b.Navigation("DonHangs");
                });
#pragma warning restore 612, 618
        }
    }
}
