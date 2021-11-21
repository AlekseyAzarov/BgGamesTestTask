using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _pauseMenu;

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
    }
}
