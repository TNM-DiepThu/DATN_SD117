using AppData.data;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddTransient<IThuongHieuServiece, ThuongHieuServiece>();
builder.Services.AddTransient<IXuatSuServiece, XuatSuServiece>();
builder.Services.AddTransient<ISanPhamServiece, SanPhamServiece>();
builder.Services.AddTransient<IAnhServiece, AnhServiece>();
builder.Services.AddTransient<IMauSacServiece, MauSacServiece>();
builder.Services.AddTransient<ISizeServiece, SizeServiece>();
builder.Services.AddTransient<IDanhMucServiece, DanhMucServiece>();
builder.Services.AddTransient<IChatLieuServiece, ChatLieuServiece>();
builder.Services.AddTransient<ISanPhamChiTietServiece, SanPhamChiTietServiece>();
builder.Services.AddTransient<IVoucherServices, VoucherServices>();
builder.Services.AddTransient<IVoucherDetailServices, VoucherDetailServices>();
builder.Services.AddTransient<IHinhThucThanhToanServices, IHinhThucThanhToanServices>();
builder.Services.AddTransient<INguoiDungServiece, NguoiDungServiece>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCS"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
