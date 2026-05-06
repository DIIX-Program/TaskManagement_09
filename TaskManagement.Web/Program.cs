using TaskManagement.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContextAccessor for accessing session in services/views
builder.Services.AddHttpContextAccessor();

// Register HttpClient and Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthApiService>();
builder.Services.AddHttpClient<ProjectApiService>();
builder.Services.AddHttpClient<TaskApiService>();
builder.Services.AddHttpClient<ReportApiService>();
builder.Services.AddHttpClient<NotificationApiService>();
builder.Services.AddHttpClient<UserApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
