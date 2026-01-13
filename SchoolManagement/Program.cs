using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data; // Đảm bảo dòng này đúng với thư mục chứa file AppDbContext.cs

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

// 2. Cấu hình DbContext (Sửa lại đoạn này một chút cho chắc chắn)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolManagementDB;Trusted_Connection=True;TrustServerCertificate=True;")); 
    // Mình thêm TrustServerCertificate=True để tránh lỗi bảo mật kết nối cục bộ

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 3. Định nghĩa đường dẫn mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();