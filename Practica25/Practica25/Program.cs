using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Practica25.Client.Pages;
using Practica25.Components;
using Practica25.Components.Account;
using Practica25.Infrastructure.Data; // Use Infrastructure for the DbContext


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// This line correctly reads the connection string we set up
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register the single DbContext for the entire application
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

// Configure ASP.NET Core Identity
builder.Services.AddAuthentication(options =>
{
     options.DefaultScheme = IdentityConstants.ApplicationScheme;
     options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

// Add this line right here
builder.Services.AddAuthorization();

// This line scans the Application assembly for MediatR handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Practica25.Application.IApplicationMarker).Assembly));

// This configures Identity to use our ApplicationUser and ApplicationDbContext
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>() // This links Identity to our DbContext
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender<ApplicationUser>, SendGridEmailSender>();


builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
     var sender = scope.ServiceProvider.GetRequiredService<IEmailSender<ApplicationUser>>();
     var testUser = new ApplicationUser { UserName = "testuser" };

     await sender.SendConfirmationLinkAsync(testUser, "adresa-ta@gmail.com", "https://localhost:5050/test-confirm");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseWebAssemblyDebugging();
     app.UseMigrationsEndPoint();
}
else
{
     app.UseExceptionHandler("/Error", createScopeForErrors: true);
     app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.MapControllers();

app.Run();
