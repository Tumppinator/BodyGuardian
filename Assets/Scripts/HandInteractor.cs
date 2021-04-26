using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInteractor : XRDirectInteractor
{

    [Header("Sounds")]
    public AudioClip bowGrabClip;
    public AudioClip arrowGrabClip;

    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ForceInteract(XRBaseInteractable interactable)
    {
        OnSelectEnter(interactable);
    }

    public void ForceDeinteract(XRBaseInteractable interactable)
    {
        OnSelectExit(interactable);
    }

    public void HandDetection(XRBaseInteractable interactable)
    {
        if (interactable is Arrow arrow)
        {
            arrow.sphereCollider.enabled = false;
        }


        if (interactable is Bow bow)
        {
            audioSource.clip = bowGrabClip;
            audioSource.Play();
        }
    }
}
