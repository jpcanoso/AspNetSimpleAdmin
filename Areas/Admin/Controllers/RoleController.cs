using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspNetSimpleAdmin.Areas.Admin.Models;
using AspNetSimpleAdmin.Models;
using Microsoft.AspNet.Identity.Owin;

namespace AspNetSimpleAdmin.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Admin/Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                list.Add(new RoleViewModel(role));
            }
            return View(list);
        }

        // GET: Admin/Role/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(new RoleViewModel(role));
        }

        // GET: Admin/Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole()
                {
                    Name = model.Name
                };
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Role/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(new RoleViewModel(role));
        }

        // POST: Admin/Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Role/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(new RoleViewModel(role));
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                await RoleManager.DeleteAsync(role);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


    }
}