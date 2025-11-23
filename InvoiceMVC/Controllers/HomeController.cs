using InvoiceMVC.Services;
using Microsoft.AspNetCore.Mvc;
using InvoiceMVC.Models;
// تأكد من إضافة الـ Namespace الخاص بالموديل إذا لزم الأمر

namespace InvoiceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicatonDbContext _invoiceService; // إضافة السيرفس

        // حقن السيرفس في البناء
        public HomeController(ILogger<HomeController> logger, ApplicatonDbContext invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            // جلب جميع الفواتير
            var invoices = _invoiceService.Invoices.ToList();

            // حساب الإحصائيات
            ViewBag.TotalInvoices = invoices.Count;
            ViewBag.TotalRevenue = invoices.Sum(i => i.Quantity * i.UnitPrice);
            ViewBag.TotalClients = invoices.Select(i => i.ClientName).Distinct().Count(); // عدد العملاء الفريدين

            // جلب آخر 5 فواتير للعرض السريع
            var recentInvoices = invoices.Take(5).ToList();

            return View(recentInvoices);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ... Error handling code
    }
}