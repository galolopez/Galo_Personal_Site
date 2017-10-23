using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Galo_Personal_Site.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace Galo_Personal_Site.Controllers
{
    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            IList<BlogPost> plist = db.Posts.ToList();

            return View(plist.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string searchStr, int? page)
        {
            var result = db.Posts.Where(p => p.Body.Contains(searchStr))
                .Union(db.Posts.Where(p => p.Title.Contains(searchStr)))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.DisplayName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.FirstName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.LastName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.UserName.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.Email.Contains(searchStr))))
                .Union(db.Posts.Where(p => p.Comments.Any(c => c.UpdateReason.Contains(searchStr))));

            int itemsPerPage = 4;
            int pageNumber = page ?? 1;

            IList<BlogPost> plist = result.ToList();

            return View(plist.ToPagedList(pageNumber, itemsPerPage));
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            BlogPost blog = new BlogPost();
            blog.Created = new DateTimeOffset(DateTime.Now);
            return View(blog);
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]        
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }

            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    var filePath = "/Uploads/";
                    var absPath = Server.MapPath("~" + filePath);
                    blogPost.MediaURL = filePath + image.FileName;
                    image.SaveAs(Path.Combine(absPath, image.FileName));
                }
                //ApplicationUser usr = db.Users.Find(User.Identity.GetUserId());
                //blogPost
                db.Posts.Add(blogPost);
                //blogPost.Created = DateTimeOffset.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    if (!image.ContentType.Contains("image"))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.UnsupportedMediaType);
                    }
                    var fileName = Path.GetFileName(image.FileName);
                    var imgPath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    var filePath = "/Uploads/";
                    var absPath = Server.MapPath("~" + filePath);
                    blogPost.MediaURL = filePath + image.FileName;
                    image.SaveAs(Path.Combine(absPath, image.FileName));
                }
                blogPost.Updated = new DateTimeOffset(DateTime.Now);
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "BlogPosts", new { id = blogPost.Id });
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
