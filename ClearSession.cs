// Renew the session
Session.Abandon();
Session.Clear();
Session.RemoveAll();

// Store the current session ID
string sessionId = Session.SessionID;

// Abandon the current session
Session.Abandon();

// Create a new session with the same session ID
HttpContext.Current.SessionID = sessionId;