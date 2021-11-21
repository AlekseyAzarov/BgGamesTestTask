using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshRenderer), typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _startDelay;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _shieldColor;
    [SerializeField] private GameObject _onDestroyAnimationObject;
    [SerializeField] private GameObject _finishParicles;

    private NavMeshAgent _navMeshAgent;
    private bool _shieldActive;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _meshRenderer = GetComponent<MeshRenderer>();

        GameDelegatesAndEvents.Instance.PlayerEnteredDeadZone += OnDeadZoneEnter;
        GameDelegatesAndEvents.Instance.PlayerEnteredFinishZone += OnFinishZoneEnter;
        GameDelegatesAndEvents.Instance.PlayerShieldActivated += ShieldOn;
        GameDelegatesAndEvents.Instance.PlayerShieldDeactivated += ShieldOff;
        GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted += RemovePlayer;

        StartCoroutine(StartMovingAfterDelay());
    }

    public void ShieldOn()
    {
        _meshRenderer.sharedMaterial.color = _shieldColor;
        _shieldActive = true;
    }

    public void ShieldOff()
    {
        _meshRenderer.sharedMaterial.color = _defaultColor;
        _shieldActive = false;
    }

    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(_startDelay);

        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(StaticGameValues.FinishZonePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AbstractInteractableZone>(out AbstractInteractableZone zone))
        {
            zone.Interact();
        }
    }

    private void OnDeadZoneEnter()
    {
        if (_shieldActive == false)
        {
            var animationObj = Instantiate(_onDestroyAnimationObject);
            animationObj.transform.position = transform.position;
            GameDelegatesAndEvents.Instance.PlayerDied?.Invoke();
            RemovePlayer();
        }
    }

    private void OnFinishZoneEnter()
    {
        GameDelegatesAndEvents.Instance.PlayerFinished?.Invoke();
        _finishParicles.SetActive(true);
    }

    private void RemovePlayer()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameDelegatesAndEvents.Instance.PlayerEnteredDeadZone -= OnDeadZoneEnter;
        GameDelegatesAndEvents.Instance.PlayerEnteredFinishZone -= OnFinishZoneEnter;
        GameDelegatesAndEvents.Instance.PlayerShieldActivated -= ShieldOn;
        GameDelegatesAndEvents.Instance.PlayerShieldDeactivated -= ShieldOff;
        GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted -= RemovePlayer;
    }
}
