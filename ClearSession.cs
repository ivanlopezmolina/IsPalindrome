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



//------>>>>

// Create a new session ID
string newSessionId = SessionIDManager.NewSessionID();

// Remove the current session
bool isRedirected = false;
bool isAdded = false;
SessionIDManager manager = new SessionIDManager();
manager.RemoveSessionID(HttpContext.Current);
manager.SaveSessionID(HttpContext.Current, newSessionId, out isRedirected, out isAdded);


