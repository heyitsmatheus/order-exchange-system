using OrderExecutionWorker.Application.Abstractions;
using QuickFix;

namespace OrderExecutionWorker.Infrastructure.Fix.Initiator;

public sealed class FixSessionManager : IFixSessionManager
{
    private SessionID? _activeSession;
    private readonly TaskCompletionSource _connectedTcs =
       new(TaskCreationOptions.RunContinuationsAsynchronously);

    public bool IsConnected => _activeSession is not null;

    public void SetActiveSession(SessionID sessionID)
    {
        _activeSession = sessionID;
        _connectedTcs.TrySetResult();
    }

    public void ClearSession(SessionID sessionID)
    {
        if (_activeSession?.Equals(sessionID) == true)
            _activeSession = null;
    }

    public bool TrySend(Message message)
    {
        if (_activeSession is null)
            return false;

        return Session.SendToTarget(message, _activeSession);
    }

    public async Task WaitUntilConnectedAsync(CancellationToken ct)
    {
        if (IsConnected)
            return;

        await _connectedTcs.Task.WaitAsync(ct);
    }
}