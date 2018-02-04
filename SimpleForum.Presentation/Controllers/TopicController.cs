using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleForum.Domain;
using SimpleForum.Domain.Entities;
using SimpleForum.Presentation.Models;

namespace SimpleForum.Presentation.Controllers
{
    public class TopicController : Controller
    {
        private readonly IUnitOfWork _unityOfWork;

        public TopicController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public IActionResult Index(int pageIndex = 1)
        {
            var pageSize = 10;
            var topics = _unityOfWork.Topics.GetTopicListItems(pageIndex, pageSize);

            return View(new TopicIndexViewModel(topics));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {            
            return View(new TopicViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(TopicViewModel topicViewModel)
        {
            var userId = Convert.ToInt32(
                User.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault().Value);

            if (ModelState.IsValid)
            {
                Topic topic = Topic.New(topicViewModel.Description, topicViewModel.Title, userId);                

                _unityOfWork.Topics.Add(topic);
                _unityOfWork.Save();

                return RedirectToAction(nameof(Index));
            }            

            return View(topicViewModel);
        }
    }
}