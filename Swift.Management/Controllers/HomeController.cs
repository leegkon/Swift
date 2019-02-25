﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swift.Core;
using Swift.Core.Consul;
using Swift.Management.Models;
using Swift.Management.Swift;

namespace Swift.Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISwiftService _swift;

        public HomeController(ISwiftService swift)
        {
            _swift = swift;
        }

        public IActionResult Index()
        {
            var cluster = _swift.GetClusters();
            return View(cluster);
        }

        public IActionResult Members(string cluster)
        {
            ViewBag.ClusterName = cluster;
            var members = _swift.GetMembers(cluster);
            return View(members);
        }

        public IActionResult Jobs(string cluster)
        {
            ViewBag.ClusterName = cluster;
            var jobConfigs = _swift.GetJobs(cluster);
            return View(jobConfigs);
        }

        public IActionResult JobRecords(string cluster, string job)
        {
            ViewBag.ClusterName = cluster;
            ViewBag.JobName = job;
            var jobRecordss = _swift.GetJobRecords(cluster, job);
            return View(jobRecordss);
        }

        public IActionResult About()
        {
            ViewBag.Message = "Swift是一个分布式作业处理框架，支持将任务分发到多台服务器并行处理，可成倍提升大量数据的处理速度。";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "https://github.com/bosima/Swift";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
