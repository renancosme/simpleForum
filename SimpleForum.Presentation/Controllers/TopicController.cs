using System;
using System.Linq;
using AutoMapper;
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

        [HttpGet]
        public IActionResult Create()
        {            
            return View(new TopicViewModel());
        }

        [HttpPost]
        public IActionResult Create(TopicViewModel topicViewModel)
        {
            if (ModelState.IsValid)
            {
                Topic topic = new Topic
                {
                    Description = topicViewModel.Description,
                    Date = DateTime.Now,
                    Title = topicViewModel.Title,
                    UserId = 2 //Pending
                };

                _unityOfWork.Topics.Add(topic);
                _unityOfWork.Save();

                return RedirectToAction("Index");
            }            

            return View(topicViewModel);
        }
    }
}