using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenSaha.Models;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace OpenSaha.Controllers
{
    public class MaclarController : Controller
    {
        private readonly SahaContext _db;
        public MaclarController(SahaContext db)
        {
            _db = db;
        }

        public IActionResult Maclar()
        {
            return View();
        }
        public IActionResult A()
        {
            return View();
        }

    }
}

