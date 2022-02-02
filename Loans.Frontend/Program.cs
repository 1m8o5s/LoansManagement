using Loans.Frontend;
using Loans.Frontend.Authentication;
using Loans.Frontend.Authentication.Contracts;
using Loans.Frontend.Common;
using Loans.Frontend.Common.Contracts;
using Loans.Frontend.Service;
using Loans.Frontend.Service.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(sp => new ApiEndpoints());
builder.Services.AddSingleton<ICookieHelper, CookieHelper>();
builder.Services.AddSingleton<IAuthState, AuthState>();
builder.Services.AddScoped<ILoanApiService, LoanApiService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();
builder.Services.AddScoped<IEnumHelperFactory, EnumHelperFactory>();
builder.Services.AddScoped<INotificationService, NotificationService>();

await builder.Build().RunAsync();
