using System;
using System.Linq;
using System.Security.Claims;
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
            var userId = Convert.ToInt32(
                User.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault().Value);
            
            Post post = Post.New(postViewModel.Description, postViewModel.TopicId, userId);
            
            _unityOfWork.Posts.Add(post);

            _unityOfWork.Save();

            return RedirectToAction("Create", postViewModel.TopicId);
        }
    }
}