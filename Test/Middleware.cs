
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
            byte[] token;
            string bearer = "";
            context.Session.TryGetValue("Token", out token);
           
          if (token != null) {      bearer = System.Text.Encoding.UTF8.GetString(token); }

            context.Request.Headers.Add("Authorization", "Bearer " +bearer);
            context.Response.Headers.Add("Authorization", "Bearer " +bearer);
              
            

            await _next(context);

        }

    }
}
