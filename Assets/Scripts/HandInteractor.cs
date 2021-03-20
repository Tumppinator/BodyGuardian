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
            //audioSource.volume = 0.5f;
            //audioSource.clip = arrowGrabClip;
            //audioSource.Play();
        }


        if (interactable is Bow bow)
        {
            audioSource.clip = bowGrabClip;
            audioSource.Play();
        }

        //if (interactable is Bow bow)
        //{
        //    HandSounds(bowGrabClip, 2.5f, 2.5f, .8f, -3);
        //}


    }


    //void HandSounds(AudioClip clip, float minPitch, float maxPitch, float volume, int id)
    //{
    //    SFXPlayer.Instance.PlaySFX(clip, transform.position, new SFXPlayer.PlayParameters()
    //    {
    //        Pitch = Random.Range(minPitch, maxPitch),
    //        Volume = volume,
    //        SourceID = id
    //    });
    //}
}
