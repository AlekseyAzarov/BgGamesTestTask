using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _pauseMenu;

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }
}
