using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using ToDo.MvcClientApp.Models;
using ToDo.MvcClientApp.Services;

namespace ToDo.MvcClientApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IToDoService _todoListService;

        public HomeController(IToDoService todoListService)
        {
            _todoListService = todoListService;
        }

        
        [AuthorizeForScopes(ScopeKeySection = "TodoList:TodoListScope")]
        public async Task<ActionResult> Index()
        {
            return View(await _todoListService.GetAsync());
        }

        
        public async Task<ActionResult> Details(int id)
        {
            return View(await _todoListService.GetAsync(id));
        }

        
        public ActionResult Create()
        {
            ToDo todo = new ToDo() {  };
            return View(todo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Content")] ToDo todo)
        {
            await _todoListService.AddAsync(todo);
            return RedirectToAction("Index");
        }

        
        public async Task<ActionResult> Edit(int id)
        {
            ToDo todo = await this._todoListService.GetAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Content")] ToDo todo)
        {
            await _todoListService.EditAsync(todo);
            return RedirectToAction("Index");
        }

        
        public async Task<ActionResult> Delete(int id)
        {
            ToDo todo = await this._todoListService.GetAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, [Bind("Id,Content")] ToDo todo)
        {
            await _todoListService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
