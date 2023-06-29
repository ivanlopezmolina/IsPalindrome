// Get the ASP.NET_SessionId cookie
HttpCookie sessionCookie = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];

// Check if the cookie exists
if (sessionCookie != null)
{
    // Set the value to empty and expiration date to a past date
    sessionCookie.Value = string.Empty;
    sessionCookie.Expires = DateTime.Now.AddDays(-1);

    // Update the cookie in the response
    HttpContext.Current.Response.SetCookie(sessionCookie);
}
