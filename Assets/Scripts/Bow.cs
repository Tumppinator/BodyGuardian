using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    public Transform notch;
    private PullInteraction pullInteraction;
    private LineRenderer lineRenderer;


    [Header("Quiver")]
    public Transform quiver;
    public Vector2 quiverOffset;

    protected override void Awake()
    {
        base.Awake();
        pullInteraction = GetComponentInChildren<PullInteraction>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            if (isSelected)
                UpdateBow(pullInteraction.PullAmount);
    }
    private void UpdateBow(float value)
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(-0.25f, -0.68f, value);
        notch.localPosition = linePosition;
        lineRenderer.SetPosition(1, linePosition);
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    public void OffsetQuiver(XRBaseInteractor interactor)
    {
        if (interactor.TryGetComponent(out XRController controller))
        {
            bool right = (controller.inputDevice.role == UnityEngine.XR.InputDeviceRole.RightHanded);
            quiver.localPosition = new Vector3(quiverOffset.x * (right ? -1 : 1), quiverOffset.y, quiver.localPosition.z);
        }
    }
}