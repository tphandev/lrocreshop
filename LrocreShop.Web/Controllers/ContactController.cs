using AutoMapper;
using LrocreShop.Model.Models;
using LrocreShop.Service;
using LrocreShop.Web.Models;
using System.Web.Mvc;

namespace LrocreShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;

        public ContactController(IContactDetailService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }

        // GET: Contact
        public ActionResult Index()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(contactViewModel);
        }
    }
}