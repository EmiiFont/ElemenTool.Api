﻿using System.Threading.Tasks;
using System.Web.Http;

namespace ElemenTool.Api.ScheduledJobs
{
    // A simple scheduled job which can be invoked manually by submitting an HTTP
    // POST request to the path "/jobs/sample".

    public class SampleJob 
    {
        //public override Task ExecuteAsync()
        //{
        //    Services.Log.Info("Hello from scheduled job!");
        //    return Task.FromResult(true);
        //}
    }
}