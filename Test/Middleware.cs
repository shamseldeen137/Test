
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test
{
    public class Middleware
    {

        private RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {


            // context.Response.StatusCode = 200;
            //context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
           
            //string bearer = "";
            //context.Session.TryGetValue("JWToken", out token);
           
          //if (token != null) {      bearer = System.Text.Encoding.UTF8.GetString(token); }

            string token = "";
            token = context.Session.GetString("JWToken");
            //   httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //  httpClient.DefaultRequestHeaders.Authorization=  new AuthenticationHeaderValue("Bearer", token);
            context.Request.Headers.Add("Authorization", $"Bearer {token}");


            await _next(context);

        }

    }
}
