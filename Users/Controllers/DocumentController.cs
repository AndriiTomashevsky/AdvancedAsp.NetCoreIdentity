﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private ProtectedDocument[] docs = new ProtectedDocument[]
        {
            new ProtectedDocument { Title = "Q3 Budget", Author = "Alice", Editor = "Joe"},
            new ProtectedDocument { Title = "Project Plan", Author = "Bob", Editor = "Alice"}
        };

        public ViewResult Index()
        {
            return View(docs);
        }

        public ViewResult Edit(string title)
        {
            return View("Index", docs.FirstOrDefault(d => d.Title == title));
        }
    }
}