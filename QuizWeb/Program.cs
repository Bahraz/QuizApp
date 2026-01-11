using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.Interfaces;
using QuizApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// Razor Pages
// ==========================
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ==========================
// Session (imiê u¿ytkownika, flow quizu)
// ==========================
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ==========================
// DbContext (TYLKO rejestracja)
// ==========================
builder.Services.AddDbContext<QuizDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// ==========================
// Repozytoria (interfejsy)
// ==========================
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();

// ==========================
// Build
// ==========================
var app = builder.Build();

// ==========================
// Middleware pipeline
// ==========================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ?? MUSI BYÆ PRZED MapRazorPages
app.UseSession();

app.UseAuthorization();

// ==========================
// Razor Pages routing
// ==========================
app.MapRazorPages();
app.MapBlazorHub();

app.Run();