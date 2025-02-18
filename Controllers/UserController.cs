using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    // Static list to store users
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    // Returns the list of users
    public ActionResult Index(string searchString)
    {
        ViewData["CurrentFilter"] = searchString;

        var users = from u in userlist
                    select u;

        if (!String.IsNullOrEmpty(searchString))
        {
            users = users.Where(u => u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        return View(users.ToList());
    }

    // GET: User/Details/5
    // Returns the details of a specific user by ID
    public ActionResult Details(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    // Returns the view to create a new user
    public ActionResult Create()
    {
        return View();
    }

    // POST: User/Create
    // Handles the creation of a new user
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    // Returns the view to edit an existing user by ID
    public ActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    // Handles the editing of an existing user
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Update other fields as necessary
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    // Returns the view to delete a user by ID
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    // Handles the deletion of a user
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}
