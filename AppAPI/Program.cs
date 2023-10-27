using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddTransient<IHinhThucThanhToanServices, HinhThucThanhToanServiece>();
builder.Services.AddTransient<INguoiDungServiece, NguoiDungServiece>();
builder.Services.AddTransient<IQuyenService, QuyenService>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCS"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),

                        };

                    });


builder.Services.AddIdentity<NguoiDung, Quyen>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddSignInManager<SignInManager<NguoiDung>>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
