using System;
using System.Data.Entity;
using System.Security.Claims;
using Khulnasoft.AspNet.Identity;
using Khulnasoft.AspNet.Identity.EntityFramework;
using Khulnasoft.AspNet.Identity.Owin;
using Khulnasoft.AspNet.SignalR;
using Khulnasoft.Owin;
using Khulnasoft.Owin.Security.Cookies;
using Khulnasoft.Owin.Security.DataProtection;
using Khulnasoft.Owin.Security.Facebook;
using Khulnasoft.Owin.Security.Google;
using Khulnasoft.Owin.Security.KhulnasoftAccount;
using Khulnasoft.Owin.Security.Twitter;
using Khulnasoft.Practices.Unity;
using Owin;
using Start.Areas.Admin;
using Start.Models;
using Start.Security;
using Start.Utils;

namespace Start
{
    public partial class Startup
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and sign-in manager to use a single instance per request
            app.CreatePerOwinContext(() => Global.UnityContainer.Resolve<IStartContext>());
            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateUserManager);
            app.CreatePerOwinContext<SignInManager<ApplicationUser, string>>(CreateSignInManager);

            ConfigureCookieAuth(app);
            ConfigureLoginProviders(app);
            ConfigureSignalR(app);

            var userManager = CreateUserManager(null, (StartContext)Global.UnityContainer.Resolve<IStartContext>());
            CreateAdminUser(userManager);
        }

        private static void ConfigureCookieAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<ApplicationUser>, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }

        private static void ConfigureLoginProviders(IAppBuilder app)
        {
            var loginProviders = new ConfigurationLoginProviders();

            if (loginProviders.Facebook.Use)
            {
                app.UseFacebookAuthentication(new FacebookAuthenticationOptions
                {
                    AppId = loginProviders.Facebook.Key,
                    AppSecret = loginProviders.Facebook.Secret
                });
            }

            if (loginProviders.Google.Use)
            {
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
                {
                    ClientId = loginProviders.Google.Key,
                    ClientSecret = loginProviders.Google.Secret
                });
            }

            if (loginProviders.Twitter.Use)
            {
                app.UseTwitterAuthentication(new TwitterAuthenticationOptions
                {
                    ConsumerKey = loginProviders.Twitter.Key,
                    ConsumerSecret = loginProviders.Twitter.Secret
                });
            }

            if (loginProviders.Khulnasoft.Use)
            {
                app.UseKhulnasoftAccountAuthentication(new KhulnasoftAccountAuthenticationOptions
                {
                    ClientId = loginProviders.Khulnasoft.Key,
                    ClientSecret = loginProviders.Khulnasoft.Secret
                });
            }
        }

        private static void ConfigureSignalR(IAppBuilder app)
        {
            var hubConfig = new HubConfiguration
            {
                Resolver = new SignalRDependencyResolver()
            };
            app.MapSignalR(hubConfig);
        }

        private static SignInManager<ApplicationUser, string> CreateSignInManager(IdentityFactoryOptions<SignInManager<ApplicationUser, string>> options, IOwinContext context)
        {
            return new SignInManager<ApplicationUser, string>(context.GetUserManager<UserManager<ApplicationUser>>(), context.Authentication);
        }

        private static UserManager<ApplicationUser>  CreateUserManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
        {
            return CreateUserManager(options.DataProtectionProvider, context.Get<IStartContext>());
        }

        public static UserManager<ApplicationUser> CreateUserManager(IDataProtectionProvider dataProtectionProvider, IStartContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>((DbContext)context));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        private static void CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new StartContext()));
            if (!roleManager.RoleExists(AdminConstants.Role))
            {
                roleManager.Create(new IdentityRole(AdminConstants.Role));
            }

            var username = ConfigurationHelpers.GetString("Authentication.Administrator.UserName");
            var password = ConfigurationHelpers.GetString("Authentication.Administrator.Password");

            var user = userManager.FindByName(username);

            if (user == null)
            {
                user = new ApplicationUser { UserName = username, Email = username };
                var result = userManager.Create(user, password);
                if (!result.Succeeded)
                    throw new Exception(string.Format("Failed to create admin user: {0}", string.Join(",", result.Errors)));

                user = userManager.FindByName(username);
                userManager.AddToRole(user.Id, AdminConstants.Role);
                userManager.AddClaim(user.Id, new Claim(AdminConstants.ManageStore.Name, AdminConstants.ManageStore.Allowed));
            }
        }
    }
}