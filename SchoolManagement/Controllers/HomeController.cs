using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers {
    public class StudentsController : Controller {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context) {
            _context = context;
        }

        // 1. TRANG DANH SÁCH
        [HttpGet] // Thêm nhãn này để Swagger hiểu đây là hàm lấy dữ liệu
        public async Task<IActionResult> Index() {
            var data = await _context.Students.Include(s => s.School).ToListAsync();
            return View(data);
        }

        // 2. TRANG TẠO MỚI (Giao diện)
        [HttpGet("Create")] 
        public IActionResult Create() {
            ViewBag.SchoolId = new SelectList(_context.Schools, "Id", "Name");
            return View();
        }

        // 3. XỬ LÝ LƯU DỮ LIỆU MỚI
        [HttpPost]
        [ValidateAntiForgeryToken] // Bảo mật chống tấn công giả mạo trang web
        public async Task<IActionResult> Create(Student student) {
            if (ModelState.IsValid) { // Kiểm tra dữ liệu nhập vào có hợp lệ không
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SchoolId = new SelectList(_context.Schools, "Id", "Name", student.SchoolId);
            return View(student);
        }

        // 4. TRANG SỬA (Giao diện) - BỔ SUNG
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return NotFound();
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            
            ViewBag.SchoolId = new SelectList(_context.Schools, "Id", "Name", student.SchoolId);
            return View(student);
        }

        // 5. XỬ LÝ CẬP NHẬT - BỔ SUNG
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student) {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid) {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SchoolId = new SelectList(_context.Schools, "Id", "Name", student.SchoolId);
            return View(student);
        }

        // 6. XỬ LÝ XÓA
        // Để nút bấm trên Web hoạt động dễ nhất, ta dùng [HttpGet] hoặc [HttpPost]
        [HttpGet("Delete/{id}")] 
        public async Task<IActionResult> Delete(int id) {
            var student = await _context.Students.FindAsync(id);
            if (student != null) {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}