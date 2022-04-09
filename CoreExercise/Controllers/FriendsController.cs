using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreExercise.Controllers
{
    public class FriendsController : Controller
    {
        // using CoreExercise.Models; => ApplicationDbContext
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // using System.Threading.Tasks; => Task
        public async Task<IActionResult> Index()
        {
            // 添加這個後重新整理，不會消失
            TempData.Keep("test3");
            return View(await _context.Friends.ToListAsync());
        }

        #region Create [Friends新增方法]
        /// <summary>
        /// Friends新增GET方法
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Friends新增POST方法
        /// </summary>
        /// <param name="model">Friends資料</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Friends model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Edit [Friends編輯方法]
        /// <summary>
        /// Friends編輯GET方法
        /// </summary>
        /// <param name="id">Friends的PK</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            var friend = await _context.Friends.FindAsync(id);

            if (friend == null)
                return Redirect("~/Error/500.html"); //或http開頭的絕對URL

            return View(friend);
        }

        /// <summary>
        /// Friends編輯POST方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Friends model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    if (!FriendExists(model.Id))
                        return Redirect("~/Error/500.html"); //或http開頭的絕對URL
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        /// <summary>
        /// Friend是否存在
        /// </summary>
        /// <param name="id">Friend的PK</param>
        /// <returns></returns>
        private bool FriendExists(int id)
        {
            // using System.Linq; => Any
            return _context.Friends.Any(e => e.Id == id);
        }
        #endregion

        #region Delete [Friends刪除方法]
        /// <summary>
        /// Friends刪除GET方法
        /// </summary>
        /// <param name="id">Friends的PK</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            var friend = await _context.Friends
                .FirstOrDefaultAsync(m => m.Id == id);

            if (friend == null)
                return Redirect("~/Error/500.html"); //或http開頭的絕對URL

            return View(friend);
        }

        /// <summary>
        /// Friends刪除POST方法
        /// </summary>
        /// <param name="id">Friends的PK</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Details [Friend細節]
        /// <summary>
        /// Friend細節
        /// </summary>
        /// <param name="id">Friend的PK</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            var friend = await _context.Friends
                .FirstOrDefaultAsync(m => m.Id == id);

            if (friend == null)
                return Redirect("~/Error/500.html"); //或http開頭的絕對URL

            return View(friend);
        }
        #endregion
    }
}
