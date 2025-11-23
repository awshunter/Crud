using InvoiceMVC.Models;
using InvoiceMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceMVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicatonDbContext context;

        public InvoicesController(ApplicatonDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int pageNumber = 1)
        {
            
            int pageSize = 5; // عدد الفواتير في كل صفحة
            int totalInvoices = context.Invoices.Count();

            var invoices = context.Invoices
                                  .OrderByDescending(inv => inv.Id)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalInvoices / pageSize);
            ViewBag.CurrentPage = pageNumber;

            
            return View(invoices);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(InvoiceDTU invoiceDTU)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var invoice = new Invoice
            {
                Number = invoiceDTU.Number,
                IssueDate = invoiceDTU.IssueDate,
                DueDate = invoiceDTU.DueDate,
                Service = invoiceDTU.Service,
                UnitPrice = invoiceDTU.UnitPrice,
                Quantity = invoiceDTU.Quantity,
                ClientName = invoiceDTU.ClientName,
                Email = invoiceDTU.Email,
                phone = invoiceDTU.phone,
                Address = invoiceDTU.Address ?? "",
            };
            context.Invoices.Add(invoice);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                return RedirectToAction("index");
            }

            var invoiceDTU = new InvoiceDTU
            {
                Number = invoice.Number,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                Service = invoice.Service,
                UnitPrice = invoice.UnitPrice,
                Quantity = invoice.Quantity,
                ClientName = invoice.ClientName,
                Email = invoice.Email,
                phone = invoice.phone,
                Address = invoice.Address,
            };
            ViewBag.InvoiceId = invoice.Id;
            return View(invoiceDTU);
        }


        [HttpPost]
        public IActionResult Edit(int id, InvoiceDTU invoiceDTU)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                return RedirectToAction("index");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            invoice.Number = invoiceDTU.Number;
            invoice.IssueDate = invoiceDTU.IssueDate;
            invoice.DueDate = invoiceDTU.DueDate;
            invoice.Service = invoiceDTU.Service;
            invoice.UnitPrice = invoiceDTU.UnitPrice;
            invoice.Quantity = invoiceDTU.Quantity;
            invoice.ClientName = invoiceDTU.ClientName;
            invoice.Email = invoiceDTU.Email;
            invoice.phone = invoiceDTU.phone;
            invoice.Address = invoiceDTU.Address ?? "";
            context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                context.SaveChanges();
                context.Invoices.Remove(invoice);

            }
            return RedirectToAction("index");
        }

      



    }
}
