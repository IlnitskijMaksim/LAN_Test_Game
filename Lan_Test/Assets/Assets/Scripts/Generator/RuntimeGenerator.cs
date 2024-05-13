using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class MapRuntimeGenerator : NetworkBehaviour
{
    public UnityEvent OnStart;

    public override void OnStartServer()
    {
        base.OnStartServer();
        OnStart?.Invoke();
    }
}
