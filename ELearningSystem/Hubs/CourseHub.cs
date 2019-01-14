using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ELearningSystem.Hubs
{
    public class CourseHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CourseHub>();
            context.Clients.All.displayCourses();
        }
    }
}