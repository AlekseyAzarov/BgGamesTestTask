using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FinishZone : AbstractInteractableZone
{
    private void Start()
    {
        _zoneColor = new Color(0.13f, 0.55f, 0.13f);
        StaticGameValues.FinishZonePosition = transform.position;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        meshRenderer.material.color = _zoneColor;
    }

    public override void Interact()
    {
        GameDelegatesAndEvents.Instance.PlayerEnteredFinishZone?.Invoke();
        gameObject.SetActive(false);
    }
}
