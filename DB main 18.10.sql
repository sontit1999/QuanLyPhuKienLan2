use master
go
drop database  QuanLyPhuKien
drop database QuanLyPhuKien
create database QuanLyPhuKien
go

use QuanLyPhuKien
go

create table NhanVien (
MaNhanVien varchar(10) not null primary key,
HoTenNhanVien nvarchar(30),
GioiTinh nvarchar(5),
NgaySinh date not null,
DiaChi nvarchar(50),
SoDienThoai varchar(11),
MatKhau varchar(15) not null,
PhanQuyen nvarchar(15),
)
go

create table LoaiSanPham (
MaLoaiSanPham varchar(10) not null primary key,
TenLoaiSanPham nvarchar(30) not null,
MoTa nvarchar(200),
)
go

create table SanPham(
MaSanPham varchar(10) not null primary key,
MaLoaiSanPham varchar(10) references LoaiSanPham(MaLoaiSanPham),
TenSanPham nvarchar(50) not null,
SoLuong int not null,
GiaNhap int not null,
GiaBan int not null,
ThoiGianBaoHanh nvarchar(20),
DonVi nvarchar(20),
MoTa nvarchar(200),
)
go

create table HoaDon (
MaHoaDon varchar(10) not null primary key,
MaNhanVien varchar(10) references NhanVien(MaNhanVien),
NgayLap date not null default getdate(),
TongTienHoaDon int,
)
go
drop table ChiTiet_HoaDon
create table ChiTiet_HoaDon (
MaChiTietHoaDon nvarchar(10) not null ,
MaHoaDon varchar(10) references HoaDon(MaHoaDon),
MaSanPham varchar(10) references SanPham(MaSanPham),
SoLuong int not null,
TienPhaiTra int,
primary key(MaChiTietHoaDon)
)
go

select GiaNhap from SanPham where MaSanPham = 'SP01'

insert into NhanVien (MaNhanVien,HoTenNhanVien,GioiTinh,NgaySinh,DiaChi,SoDienThoai,MatKhau,PhanQuyen) 
values
('NV01',N'Nguyen Thai Nguyen',N'Nam','1995-10-15',N'Dong Thap','0939205421','ntn421','nhan vien')
('admin',N'Nguyen Vi Khang',N'Nam','1995-08-17',N'Soc Trang','09394121584','admin','admin'),
,

('NV02',N'Nguyen Duong Thai Ngoc',N'Nam','1995-05-03',N'Soc Trang','0855633053','ndtn053','nhan vien')

update NhanVien set PhanQuyen= 'nhan vien' where MaNhanVien = 'NV02'
select * from ChiTiet_HoaDon
select * from HoaDon
select * from NhanVien
select * from SanPham
select * from LoaiSanPham
insert into HoaDon values
('HD01','NV01','2020-10-10',5000)
insert into ChiTiet_HoaDon values
('CTHD09','HD2797','SP02',50,50000)
delete  from HoaDon
delete from ChiTiet_HoaDon