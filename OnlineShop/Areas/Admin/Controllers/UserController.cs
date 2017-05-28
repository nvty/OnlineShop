using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page=1, int pageSize=1)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMD5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMD5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thành công");
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //Truong hop nhập password thì mới update còn không nhập thi vẫn giữ như cũ
                //Kiểm tra giá trị password khác rổng thì mới update
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMD5Pas;
                }
                
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user thành công");
                }
            }

            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}