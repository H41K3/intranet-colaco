using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var exigirHttps = builder.Configuration.GetValue("Security:RequireHttps", !builder.Environment.IsDevelopment());
var proxyConfig = builder.Configuration.GetSection("ForwardedHeaders");
var usarProxy = proxyConfig.GetValue<bool>("Enabled");

if (usarProxy)
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownIPNetworks.Clear();
        options.KnownProxies.Clear();
        foreach (var ip in proxyConfig.GetSection("KnownProxies").Get<string[]>() ?? [])
            options.KnownProxies.Add(IPAddress.Parse(ip));
    });
}

builder.Services.AddControllers();
var app = builder.Build();

if (usarProxy) app.UseForwardedHeaders();

if (exigirHttps)
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.Use(async (ctx, next) =>
{
    var headers = ctx.Response.Headers;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "DENY";
    headers["Referrer-Policy"] = "no-referrer";
    headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
    headers["Cross-Origin-Opener-Policy"] = "same-origin";

    if (ctx.Request.Path.StartsWithSegments("/api"))
        headers["Cache-Control"] = "no-store";

    await next();
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();