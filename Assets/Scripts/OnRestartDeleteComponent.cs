using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRestartDeleteComponent : MonoBehaviour
{
    private void Start()
    {
        GameDelegatesAndEvents.Instance.PlayerDestroyedGameRestarted += OnRestart;

    }

    private void OnRestart()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameDelegatesAndEvents.Instance.PlayerDestroyedGameRestarted -= OnRestart;
    }
}
