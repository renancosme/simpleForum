using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleForum.Domain;
using SimpleForum.Domain.Entities;
using SimpleForum.Presentation.Models;

namespace SimpleForum.Presentation.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unityOfWork;

        public PostController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        
        [HttpGet]
        public IActionResult Create(int id)
        {
            var topicAndPosts = _unityOfWork.Topics.GetTopicWithPosts(id);
            return View(new PostViewModel(topicAndPosts));
        }

        [HttpPost]
        public IActionResult Create(PostViewModel postViewModel)
        {
            Post post = new Post {
                Description = postViewModel.Description,
                Date = DateTime.Now,
                TopicId = postViewModel.TopicId,
                UserId = 2 //Pending
            };

            _unityOfWork.Posts.Add(post);

            _unityOfWork.Save();

            return RedirectToAction("Create", postViewModel.TopicId);
        }
    }
}