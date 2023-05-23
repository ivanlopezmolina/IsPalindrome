// To add JWT authentication to a .NET Framework 4.7 web application and create two APIs, one for generating the token and another for retrieving stored values, follow these steps:

// Install required NuGet packages:

// System.IdentityModel.Tokens.Jwt
// Microsoft.Owin.Security.Jwt
// Microsoft.AspNet.WebApi.Owin
// Open the Web.config file and make sure the following settings are present within the <configuration> element:

// <appSettings>
//   <add key="Issuer" value="your_issuer" />
//   <add key="Audience" value="your_audience" />
//   <add key="Secret" value="your_secret_key" />
// </appSettings>
// Create a new JwtManager class to handle token generation and validation:
// csharp
// Copy code
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

public class JwtManager
{
    private static readonly string secretKey = ConfigurationManager.AppSettings["Secret"];
    private static readonly string issuer = ConfigurationManager.AppSettings["Issuer"];
    private static readonly string audience = ConfigurationManager.AppSettings["Audience"];

    public static string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "your_username")
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static ClaimsPrincipal GetPrincipal(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }
}
// Create a new API controller named TokenController to generate the token:
// csharp
// Copy code
using System.Web.Http;

public class TokenController : ApiController
{
    [HttpGet]
    [Route("api/token")]
    public IHttpActionResult GetToken()
    {
        var token = JwtManager.GenerateToken();
        return Ok(token);
    }
}
// Create another API controller named ValuesController to retrieve stored values:
// csharp
// Copy code
using System.Web.Http;

public class ValuesController : ApiController
{
    [HttpGet]
    [Route("api/values")]
    [Authorize]
    public IHttpActionResult GetValues()
    {
        // Retrieve and return the stored values
        return Ok("Your stored values");
    }
}
// Finally, configure the Web API routes in the WebApiConfig.cs file:
// csharp
// Copy code
using System.Web.Http;

public class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Route for token generation
        config.Routes.MapHttpRoute(
            name: "Token",
            routeTemplate: "api/token",
            defaults: new { controller = "





// how can I store user name and email on the JWT token ?

// To store additional information like the user name and email in the JWT token, you can include them as custom claims. Here's an updated version of the GenerateToken method in the JwtManager class:

// csharp
// Copy code
public static string GenerateToken(string userName, string email)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(secretKey);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, email)
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        Issuer = issuer,
        Audience = audience,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
// With this modification, the GenerateToken method now takes the userName and email as parameters. You can call this method from your TokenController and pass the appropriate values when generating the token.

// For example:


[HttpGet]
[Route("api/token")]
public IHttpActionResult GetToken()
{
    string userName = "your_username";
    string email = "your_email@example.com";

    var token = JwtManager.GenerateToken(userName, email);
    return Ok(token);
}
// Now, when you decode and validate the token in your API endpoints, you can access the user name and email claims from the ClaimsPrincipal object.