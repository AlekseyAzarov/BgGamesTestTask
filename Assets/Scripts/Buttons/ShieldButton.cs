using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _shieldLifeTime;

    private bool _shieldActive;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameDelegatesAndEvents.Instance.PlayerShieldActivated?.Invoke();
        _shieldActive = true;

        StartCoroutine(DeactivateShieldAfterLifeTimeExpired());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DeactivateShield();
    }

    private IEnumerator DeactivateShieldAfterLifeTimeExpired()
    {
        yield return new WaitForSeconds(_shieldLifeTime);

        DeactivateShield();
    }

    private void DeactivateShield()
    {
        if (_shieldActive == true)
        {
            GameDelegatesAndEvents.Instance.PlayerShieldDeactivated?.Invoke();
            _shieldActive = false;
        }
    }
}
