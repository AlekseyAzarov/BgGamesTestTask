using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreneShadingComponent : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _shadingSpeed;
    [SerializeField] private Image _shadingPlane;

    private Color _panelColor;
    private float _alpha;

    private void OnEnable()
    {
        _panelColor = _shadingPlane.color;
        _alpha = 0;
    }

    private void Update()
    {
        _alpha += _shadingSpeed * Time.deltaTime;
        _panelColor.a = Mathf.Clamp01(_alpha);
        _shadingPlane.color = _panelColor;

        if (_alpha > 1)
        {
            _alpha = 1f;
            _shadingSpeed *= -1;
            GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted?.Invoke();
        }
        if (_alpha < 0)
        {
            _alpha = 0f;
            _shadingSpeed *= -1;
            gameObject.SetActive(false);
        }
    }
}
