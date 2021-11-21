using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] private ScreneShadingComponent _shadingComponent;
    [SerializeField] private float _onPlayerDestroyRestartDelay;

    private void Start()
    {
        GameDelegatesAndEvents.Instance.PlayerDied += RestartOnPlayerDestroy;
        GameDelegatesAndEvents.Instance.PlayerFinished += RestartOnPlayerFinished;
    }

    private void RestartOnPlayerDestroy()
    {
        StartCoroutine(OnPlayerDestroyRestartWithDelay());
    }

    private void RestartOnPlayerFinished()
    {
        _shadingComponent.gameObject.SetActive(true);
    }

    private IEnumerator OnPlayerDestroyRestartWithDelay()
    {
        yield return new WaitForSeconds(_onPlayerDestroyRestartDelay);

        GameDelegatesAndEvents.Instance.PlayerDestroyedGameRestarted?.Invoke();
    }

    private void OnDestroy()
    {
        GameDelegatesAndEvents.Instance.PlayerDied -= RestartOnPlayerDestroy;
        GameDelegatesAndEvents.Instance.PlayerFinished -= RestartOnPlayerFinished;
    }
}
