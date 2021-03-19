using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using KC_Decoupage.Models;

namespace KC_Decoupage.Controllers
{
	[Authorize(Roles = RoleName.Admin)]
    
	public class UserAccountController : Controller
	{
		private ApplicationDbContext _context;

	    internal static string GetStringSha256Hash(string password)
	    {
	        byte[] salt;
	        byte[] buffer2;
	        if (password == null)
	        {
	            throw new ArgumentNullException("password");
	        }
	        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
	        {
	            salt = bytes.Salt;
	            buffer2 = bytes.GetBytes(0x20);
	        }
	        byte[] dst = new byte[0x31];
	        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
	        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
	        return Convert.ToBase64String(dst);
	    }

		public UserAccountController()
		{
			_context = new ApplicationDbContext();
		}

		//
		// GET: /UserAccount/
		
		public ActionResult UserManager()
		{
			var users=_context.Users.ToList();
			return View(users);
		}

	    [Route("UserAccount/Delete/{id}")]
	    public ActionResult Delete(string id)
	    {
	        var user = _context.Users.Single(u => u.Id==id);

	        if (user == null)
	           return HttpNotFound();

	        _context.Users.Remove(user);
	        _context.SaveChanges();

	        return RedirectToAction("UserManager");
	    }
	    
	    [Route("UserAccount/Update/{id}")]

	    public ActionResult Update(string id)
	    {
	        var user = _context.Users.Single(u => u.Id == id);

	        if (user == null)
	            return HttpNotFound();
	        
	        var model = new RegisterViewModel
	        {
                UserName = user.UserName,
                Password =  user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                Id = user.Id
                
                
	        };


	       


	        return View("Update", model);
	    }

	    public ActionResult SaveUpdate(RegisterViewModel model)
	    {


             var user = _context.Users.Single(u => u.Id == model.Id);

	        if (user == null)
	            return HttpNotFound();

	        user.UserName = model.UserName;
	        user.PasswordHash = GetStringSha256Hash(model.Password);
	        _context.SaveChanges();





            return RedirectToAction("UserManager");

	    }


	    public ActionResult CreateUser()
	    {

	        return View();
	    }

	    public ActionResult SaveCreate(RegisterViewModel model)
	    {


	        var user = new ApplicationUser
	        {
	            UserName = model.UserName, PasswordHash = GetStringSha256Hash(model.Password)
	        };


	        _context.Users.Add(user);

	        _context.SaveChanges();





	        return RedirectToAction("UserManager");

	    }

	}
}