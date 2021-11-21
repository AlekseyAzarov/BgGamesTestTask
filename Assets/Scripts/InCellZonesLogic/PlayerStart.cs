using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Start()
    {
        SpawnPlayer();
        GameDelegatesAndEvents.Instance.PlayerDestroyedGameRestarted += SpawnPlayer;
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(_player.gameObject);
        player.transform.position = transform.position;
    }

    private void OnDestroy()
    {
        GameDelegatesAndEvents.Instance.PlayerDestroyedGameRestarted -= SpawnPlayer;
    }
}
