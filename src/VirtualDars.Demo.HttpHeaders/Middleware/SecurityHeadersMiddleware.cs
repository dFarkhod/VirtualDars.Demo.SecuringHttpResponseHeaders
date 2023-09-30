using Microsoft.AspNetCore.Http;

namespace VirtualDars.Demo.HttpHeaders.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Frame-Options", "DENY"); // iframe'ni ichida boshqa websitelarni chaqirishni taqiqlaydi (clickjacking).
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff"); // browser'ga http javobning MIME typeni aniqlashni taqiqlanadi.
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'"); // resurslarni (css/js/...) faqatgina o'zimizni web serverdan yuklash mumkin!
            context.Response.Headers.Add("Referrer-Policy", "no-referrer"); // boshqa saytga soʻrov joʻnatilganda referrer header qo'yilmaydi.
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block"); // browser cross site scripting qilishni taqiqlaydi.

            await _next(context);
        }
    }
}
