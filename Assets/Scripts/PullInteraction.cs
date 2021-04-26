using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullInteraction : XRBaseInteractable
{
    public float PullAmount { get; private set; } = 0.0f;
    public Transform start, end;
    private XRBaseInteractor pullingInteractor = null;

    [Header("Polish")]
    public LineRenderer stringLine;
    [ColorUsage(true, true)]
    public Color stringNormalCol, stringPulledCol;

    protected override void Awake()
    {
        base.Awake();
    }
    public void ForceInteract(XRBaseInteractor interactor)
    {
        OnSelectEnter(interactor);
    }
    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        pullingInteractor = interactor;

        if (pullingInteractor.TryGetComponent(out XRController controller))
            HapticManager.Impulse(.5f, .05f, controller.inputDevice);

    }
    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        pullingInteractor = null;
        PullAmount = 0f;

        stringLine.material.SetColor("_EmissionColor", stringNormalCol);
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                PullAmount = CalculatePull(pullPosition);

                if (pullingInteractor.TryGetComponent(out XRController controller) && PullAmount > .3f)
                    HapticManager.Impulse(PullAmount / 5f, .05f, controller.inputDevice);

                stringLine.material.SetColor("_EmissionColor",
                    Color.Lerp(stringNormalCol, stringPulledCol, PullAmount));
            }
        }
    }
    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }
}
