using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractableZone : MonoBehaviour
{
    protected Color _zoneColor;

    public abstract void Interact();
}
