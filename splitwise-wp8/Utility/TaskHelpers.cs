﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Utility
{
    public static class TaskHelpers
    {
        public static Task<IRestResponse> ExecuteRequestAsync(this RestClient client, RestRequest request) {
            var tcs = new TaskCompletionSource<IRestResponse>();

            client.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                {
                    tcs.TrySetException(response.ErrorException);
                }
                else
                {
                    tcs.TrySetResult(response);
                }
            });

            return tcs.Task;
        }
    }
}
