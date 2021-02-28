﻿using DowntimeAlerterWeb.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace DowntimeAlerterWeb.Services
{
    public class MonitorLoop
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly CancellationToken _cancellationToken;

        public MonitorLoop(IBackgroundTaskQueue taskQueue,
            ILogger<MonitorLoop> logger,
            IHostApplicationLifetime applicationLifetime, IHttpClientFactory clientFactory)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _clientFactory = clientFactory;
            _cancellationToken = applicationLifetime.ApplicationStopping;
        }

        public void StartMonitorLoop()
        {
            _logger.LogInformation("Monitor Loop is starting.");

            // Run a console loop in a background thread
            Task.Run(() => Monitor());
        }

        public void Monitor()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                // Application stopping, DO cleanup, teardown here.
            }
        }

        public void AddTask(SprintTaskInformation task)
        {
            Func<CancellationToken, Task> createTask = async token =>
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, task.Url);
                using var client = _clientFactory.CreateClient();
                Exception exception = default;
                HttpResponseMessage response = default;

                try
                {
                    response = await client.SendAsync(request);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                var result = SprintTaskResult.New(response.StatusCode, exception);

            };

            _logger.LogInformation("Adding task to the queue: Monitor#{Id} {Url}", task.Id, task.Url);
            _taskQueue.QueueBackgroundWorkItem(createTask);
        }
    }
}
