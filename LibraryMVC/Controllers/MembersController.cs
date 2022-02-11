using LibraryMVC.BL;
using LibraryMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.UI.Controllers
{
    public class MembersController : Controller
    {
        private readonly IService<Member> _membersService;

        public MembersController(IService<Member> membersService)
        {
            _membersService = membersService;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _membersService.GetAllAsync();

            return View(members);
        }

        public IActionResult AddMember()
        {
            var member = new Member();
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            if (!await _membersService.AddAsync(member))
            {
                return View(member);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var member = await _membersService.GetByIdAsync(id);
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member member)
        {
            if (!await _membersService.UpdateAsync(member))
            {
                return View(member);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _membersService.DeleteByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}
