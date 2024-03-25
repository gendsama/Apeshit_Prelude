using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableColliderOnGrab : MonoBehaviour
{
    private Collider _collider;
    private XRGrabInteractable _grabInteractable;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _grabInteractable = GetComponent<XRGrabInteractable>();

        if (_collider == null)
        {
            Debug.LogError("Collider component not found on GameObject.");
            enabled = false;
            return;
        }

        if (_grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component not found on GameObject.");
            enabled = false;
            return;
        }

        _grabInteractable.onSelectEnter.AddListener(OnGrab);
        _grabInteractable.onSelectExit.AddListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        _collider.enabled = false;
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        _collider.enabled = true;
    }
}

