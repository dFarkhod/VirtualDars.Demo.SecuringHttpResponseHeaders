using VirtualDars.Demo.HttpHeaders.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false); // Kesterldan qaytadigan http javoblarga server header'ni qo'shmaydi

builder.Services.AddControllers();

builder.Services.AddHsts(x =>
{
    x.Preload = true;
    x.IncludeSubDomains = true;
    x.MaxAge = TimeSpan.FromDays(60);
});


var app = builder.Build();

app.UseHsts(); // browser'ga HTTPS ishlatilishi kerakligini bildiradi

app.UseHttpsRedirection(); // web srverga HTTP dan kelgan so'rovlarni HTTPS ga o'tkazaib yuboradi

app.UseMiddleware<SecurityHeadersMiddleware>(); // custum middleware


app.UseAuthorization();


app.MapControllers();

app.Run();
