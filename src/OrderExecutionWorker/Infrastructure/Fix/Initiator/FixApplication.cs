using OrderExecutionWorker.Application.Abstractions;
using QuickFix;
using QuickFix.Fields;

namespace OrderExecutionWorker.Infrastructure.Fix.Initiator;

public sealed class FixApplication(
    IFixSessionManager sessionManager,
    ILogger<FixApplication> logger) : IApplication
{
    public void OnCreate(SessionID sessionID)
    {
        logger.LogInformation("FIX session created {Session}", sessionID);
    }

    public void OnLogon(SessionID sessionID)
    {
        logger.LogInformation("FIX logon {Session}", sessionID);
        sessionManager.SetActiveSession(sessionID);
    }

    public void OnLogout(SessionID sessionID)
    {
        logger.LogWarning("FIX logout {Session}", sessionID);
        sessionManager.ClearSession(sessionID);
    }

    public void ToAdmin(Message message, SessionID sessionID)
    {
    }

    public void FromAdmin(Message message, SessionID sessionID)
    {
    }

    public void ToApp(Message message, SessionID sessionID)
    {
        logger.LogDebug("FIX OUT {MsgType}", message.Header.GetString(Tags.MsgType));
    }

    public void FromApp(Message message, SessionID sessionID)
    {
        logger.LogDebug("FIX IN {MsgType}", message.Header.GetString(Tags.MsgType));
    }
}