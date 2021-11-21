using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDelegatesAndEvents : MonoBehaviour
{
    public Action PlayerEnteredDeadZone;
    public Action PlayerEnteredFinishZone;
    public Action PlayerShieldActivated;
    public Action PlayerShieldDeactivated;
    public Action PlayerFinishedGameRestarted;
    public Action PlayerDestroyedGameRestarted;
    public Action PlayerDied;
    public Action PlayerFinished;

    public static GameDelegatesAndEvents Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        PlayerEnteredDeadZone = null;
        PlayerEnteredFinishZone = null;
        PlayerShieldActivated = null;
        PlayerShieldDeactivated = null;
        PlayerFinishedGameRestarted = null;
        PlayerDied = null;
        PlayerFinished = null;
    }
}
