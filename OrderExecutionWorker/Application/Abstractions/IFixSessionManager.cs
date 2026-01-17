using QuickFix;

namespace OrderExecutionWorker.Application.Abstractions;

public interface IFixSessionManager
{
    bool IsConnected { get; }
    Task WaitUntilConnectedAsync(CancellationToken ct);
    void SetActiveSession(SessionID sessionID);
    void ClearSession(SessionID sessionID);
    bool TrySend(Message message);
}
