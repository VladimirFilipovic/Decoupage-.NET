using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Web;
using System.Web.Mvc;
using KC_Decoupage.Models;
using Microsoft.AspNet.Identity;
using KC_Decoupage.ViewModels;

namespace KC_Decoupage.Controllers
{
	public class PostsController : Controller
	{
		//
		// GET: /Posts/private ApplicationDbContext _context;
		private ApplicationDbContext _context;
		public PostsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
		//[Route("posts/createdBy/{creator}")]
		public ActionResult Index()
		{
			var posts = _context.Post.ToList();

			//if (User.IsInRole("Admin"))
				//return View("IndexAdmin",posts);
			 
			return View(posts);
			//return RedirectToAction("index","Home",new{objekat kao argument za kaciju})-redirektuje te na akciju,controler!
		}
		[Authorize]
		public ActionResult PostForm()
		{
			return View();
		}
		[HttpPost] //action can only be called with http post not get
		[ValidateAntiForgeryToken]
		public ActionResult Save(Post post)
		{
			
		  
			
			if (post.Id == 0)
			{
				if (post.File == null)
				{
					return View("PostForm", post);
				}

				var extensionCheck = Path.GetExtension(post.File.FileName);
				var isImage = extensionCheck == ".jpeg" || extensionCheck == ".jpg" || extensionCheck == ".png";

				if (!isImage)
				{
					return View("PostForm", post);

				}

				post.Creator = User.Identity.GetUserName();
				post.LikedBy = "";
				post.Commentators = "";
				post.DislikedBy = "";

				

					string fileName = Path.GetFileNameWithoutExtension(post.File.FileName);
					string extension = Path.GetExtension(post.File.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					post.ImagePath = "~/Images/" + fileName;
					fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
					post.File.SaveAs(fileName);

				
				_context.Post.Add(post);

			}
			else
			{
				var postInDb = _context.Post.Single(p => p.Id == post.Id);

				postInDb.Title = post.Title;
				
				postInDb.Content = post.Content;

			  //  postInDb.File = post.File;

				if (post.File != null)
				{
					var extensionCheck = Path.GetExtension(post.File.FileName);
					var isImage = extensionCheck == ".jpeg" || extensionCheck == ".jpg" || extensionCheck == ".png";

					if (!isImage)
					{
						return View("PostForm", post);

					}


					string fileName = Path.GetFileNameWithoutExtension(post.File.FileName);
					string extension = Path.GetExtension(post.File.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					postInDb.ImagePath = "~/Images/" + fileName;
					fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
					postInDb.File = post.File;
					post.File.SaveAs(fileName);
				}

			}
			_context.SaveChanges();

			return RedirectToAction("MyGallery","Posts",new{creator=User.Identity.GetUserName()});
		}
		[Route("Posts/MyGallery/{creator}")]

		
		public ActionResult MyGallery(string creator)
		{
			var posts = _context.Post.Where(p => p.Creator == creator).ToList();

			return View(posts);

		}
		[Route("Posts/ArtistGallery/{creator}")]

		public ActionResult ArtistGallery(string creator)
		{
			var posts = _context.Post.Where(p => p.Creator == creator).ToList();

			return View(posts);

		}
		[Authorize]
		public ActionResult Update(int id)
		{
		   var post = _context.Post.Single(p => p.Id == id);

			if(post == null)
			return HttpNotFound();


			return View("PostForm",post);
		}

		public ActionResult Like(int id)
		{
			var post = _context.Post.Single(p => p.Id == id);

			if (post == null)
				return Json(new { success = false, responseText = "The attached file is not supported." }, JsonRequestBehavior.AllowGet);

			if(post.LikedBy.Contains( User.Identity.GetUserName()))
				return Json(new { success = false, responseText = "You already liked this post." }, JsonRequestBehavior.AllowGet);

		    if (post.DislikedBy.Contains(User.Identity.GetUserName()))
		    {
		        post.Dislikes--;
		        post.DislikedBy=post.DislikedBy.Replace(User.Identity.GetUserName(), "");

		    }

		    post.Likes++;

			post.LikedBy+=(User.Identity.GetUserName());
			_context.SaveChanges();

		    return Json(new { success = true, Likes = post.Likes, Dislikes = post.Dislikes, Title = post.Title, Content = post.Content }, JsonRequestBehavior.AllowGet);

		}

	    public ActionResult Dislike(int id)
	    {
	        var post = _context.Post.Single(p => p.Id == id);

	        if (post == null)
	            return Json(new { success = false, responseText = "The attached file is not supported." }, JsonRequestBehavior.AllowGet);

	        if (post.DislikedBy.Contains(User.Identity.GetUserName()))
	            return Json(new { success = false, responseText = "You already disliked this post." }, JsonRequestBehavior.AllowGet);

	        if (post.LikedBy.Contains(User.Identity.GetUserName()))
	        {
	            post.Likes--;
	            post.LikedBy=post.LikedBy.Replace(User.Identity.GetUserName(), "");
	        }

	        post.Dislikes++;

	        post.DislikedBy += (User.Identity.GetUserName());
	        _context.SaveChanges();

	        return Json(new { success = true, Likes = post.Likes, Dislikes = post.Dislikes, Title = post.Title,Content=post.Content }, JsonRequestBehavior.AllowGet);

	    }

		[Route("Posts/Delete/{id}")]

		public ActionResult Delete(int id)
		{
			var post = _context.Post.Single(u => u.Id == id);

			if (post == null)
				return HttpNotFound();

			_context.Post.Remove(post);
			_context.SaveChanges();

			return RedirectToAction("Index"); //ipak se potrudi da to ide preko api-a
		}

        public ActionResult ShowComments(int id)
        {
            return Json(new { success = true, postId=id }, JsonRequestBehavior.AllowGet);

        }

	    public ActionResult DeleteComment(int id, string comment)
	    {
	        var post = _context.Post.Single(u => u.Id == id);

	        if (post == null)
	            return Json(new {success = false, responseText = "Invalid request"}, JsonRequestBehavior.AllowGet);
	        if (string.IsNullOrEmpty(comment))
	            return Json(new { success = false, responseText = "Invalid request" }, JsonRequestBehavior.AllowGet);

	        post.Comments = post.Comments.Replace(comment,"");



	        _context.SaveChanges();

	        return Json(new { success = true, postId = id }, JsonRequestBehavior.AllowGet);

	    }

	    public ActionResult Comment(int id, string comment)
	    {
	        var post = _context.Post.Single(u => u.Id == id);
	        string newComment;

	        if (post == null)
	            return Json(new { success = false, responseText = "Invalid request" }, JsonRequestBehavior.AllowGet);
	        if (string.IsNullOrEmpty(comment))
	            return Json(new { success = false, responseText = "Invalid request" }, JsonRequestBehavior.AllowGet);
	        var startsWithWhiteSpace = char.IsWhiteSpace(comment, 0);
	        if (startsWithWhiteSpace)
	            return Json(new { success = false, responseText = "Invalid request" }, JsonRequestBehavior.AllowGet);



	        if (string.IsNullOrEmpty(post.Comments))
	        {

	            newComment = User.Identity.GetUserName() + ": " + comment;

	            post.Comments = newComment + "`";

	        }
	        else
	        {

	            newComment = User.Identity.GetUserName() + ": " + comment;

	            post.Comments += newComment + "`";
	        }

	        _context.SaveChanges();

	        return Json(new { success = true, postId = id, Comment = newComment }, JsonRequestBehavior.AllowGet);

	    }




	  

	  
	}
}