using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyAfterRelease : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectExited.AddListener(StartDestroy);
    }

    private void OnDestroy()
    {
        grabInteractable.onSelectExited.RemoveListener(StartDestroy);
    }

    private void StartDestroy(XRBaseInteractor interactor)
    {
        StartCoroutine(DestroyAfterSeconds(3));
    }

    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

