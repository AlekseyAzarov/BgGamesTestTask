using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DeadZone : AbstractInteractableZone
{
    private void Start()
    {
        _zoneColor = new Color(0.7f, 0.13f, 0.13f);
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        meshRenderer.material.color = _zoneColor;
    }

    public override void Interact()
    {
        GameDelegatesAndEvents.Instance.PlayerEnteredDeadZone?.Invoke();
        gameObject.SetActive(false);
    }
}
