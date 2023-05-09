public class InMemoryUserStore : IUserStore<ApplicationUser>
{
    private readonly List<ApplicationUser> _users;

    public InMemoryUserStore()
    {
        _users = new List<ApplicationUser>();
    }

    public Task CreateAsync(ApplicationUser user)
    {
        _users.Add(user);
        return Task.FromResult(0);
    }

    public Task UpdateAsync(ApplicationUser user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
        }

        return Task.FromResult(0);
    }

    public Task DeleteAsync(ApplicationUser user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            _users.Remove(existingUser);
        }

        return Task.FromResult(0);
    }

    public Task<ApplicationUser> FindByIdAsync(string userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }

    public Task<ApplicationUser> FindByNameAsync(string userName)
    {
        var user = _users.FirstOrDefault(u => u.UserName == userName);
        return Task.FromResult(user);
    }

    public void Dispose()
    {
        // Nothing to dispose.
    }
}



//////


public void Configuration(IAppBuilder app)
{
    app.CreatePerOwinContext(() => new InMemoryUserStore());
    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

    // Configure authentication middleware.
    app.UseCookieAuthentication(new CookieAuthenticationOptions
    {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login"),
        Provider = new CookieAuthenticationProvider
        {
            OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                validateInterval: TimeSpan.FromMinutes(30),
                regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
        }
    });

    // Configure external authentication middleware.
    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

    // Configure Identity Manager middleware.
    app.UseIdentityManager(new IdentityManagerOptions
    {
        Factory = new IdentityManagerServiceFactory
        {
            UserService = new Registration<IUserStore<ApplicationUser>>(resolver => app.GetPerOwinContext<IUserStore<ApplicationUser>>())
        }
    });
}






////////




public class ApplicationUserManager : UserManager<ApplicationUser>
{
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
    {
    }

    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    {
        var manager = new ApplicationUserManager(context.Get<IUserStore<ApplicationUser>>());

        // Configure validation logic for usernames, passwords, etc.
        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
        };

        // Configure validation logic for passwords.
        manager.PasswordValidator = new PasswordValidator
        {
            RequiredLength = 6,
            RequireNonLetterOrDigit = false,
            RequireDigit = true,
            RequireLowercase = true,
            RequireUppercase = true,
        };

        // Configure user lockout defaults.
        manager.UserLockoutEnabledByDefault = true;
        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        manager.MaxFailedAccessAttemptsBeforeLockout = 5;

        // Configure two factor authentication.
        manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
        {
            MessageFormat = "Your security code is: {0}"
        });
        manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
        {
            Subject = "Security Code",
            BodyFormat = "Your security code is: {0}"
        });
        manager.EmailService = new EmailService();
        manager.SmsService = new SmsService();

        // Configure the data protection provider, if necessary.
        var dataProtectionProvider = options.DataProtectionProvider;
        if (dataProtectionProvider != null)
        {
            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        }

        return manager;
    }
}
