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

using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;


// Generate a new session ID
string newSessionId = SessionIDManager.CreateSessionID(System.Web.HttpContext.Current);

// Invalidate the current session
Session.Clear();
Session.Abandon();

// Set the new session ID
SessionIDManager manager = new SessionIDManager();
bool redirected;
bool isAdded;
manager.SaveSessionID(System.Web.HttpContext.Current, newSessionId, out redirected, out isAdded);

//-----> 2

// Generate a new session ID
string newSessionId = SessionIDManager.CreateSessionID(Context);

// Save the current session state
SessionStateItemCollection sessionItems = new SessionStateItemCollection();
foreach (string key in Session.Keys)
{
    sessionItems[key] = Session[key];
}

// Abandon the current session
Session.Abandon();

// Assign the new session ID
SessionIDManager manager = new SessionIDManager();
bool redirected = false;
string newUrl = manager.GetSessionID(Context, newSessionId, out redirected);
SessionIDManager.SaveSessionID(Context, newSessionId, out redirected);

// Restore the session state
SessionStateUtility.LoadSessionStateFromItemCollection(Context, sessionItems);

// Renew the session data
// ...
