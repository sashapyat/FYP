
using Core.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using VirtualLibrary.Models;
using VirtualLibrary.Service;
using Stripe;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    var connectionString = builder.Configuration.GetConnectionString("AppDbConnection");

    builder.Services.AddControllersWithViews();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));;


    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));

    StripeConfiguration.SetApiKey(builder.Configuration.GetSection("Stripe")["SecretKey"]);
    builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));


    builder.Services.AddScoped<IUserDataService, UserDataService>();
    builder.Services.AddScoped<IBooksService, BooksService>();
    builder.Services.AddScoped<IDigitalLiscenceService, DigitalLiscenceService>();
    builder.Services.AddScoped<IAuthorsService, AuthorsService>();
    builder.Services.AddHostedService<HostedService>();


    builder.Services.AddDistributedMemoryCache(); //This way ASP.NET Core will use a Memory Cache to store session variables
    builder.Services.AddSession();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    });

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Default SignIn settings.
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    });


    builder.Services.Configure<SMTPConfigModel>(
        builder.Configuration.GetSection(SMTPConfigModel.SMTPConfig));
    builder.Services.AddScoped<IEmailService, EmailService>();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler();
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseStatusCodePagesWithReExecute("/Error/{0}");
    }

    /*app.UseStatusCodePages();*/

    app.UseSession();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication(); ;

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{

    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}