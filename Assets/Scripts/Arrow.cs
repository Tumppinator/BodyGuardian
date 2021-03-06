using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Arrow : XRGrabInteractable
{
    public float speed = 1000f;
    public Transform tip;
    bool inAir = false;
    Vector3 lastPosition = Vector3.zero;
    private Rigidbody rb;
    public Collider sphereCollider;
    bool collided = false;
    bool collidedWithEnemy = false;
    Score score;
    TP_Player TP_Player;
    EnemySpawner spawner;


    [Header("Sound")]
    [SerializeField] AudioClip launchClip;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip hitEnemyClip;

    AudioSource audioSource;
    
    protected override void Awake()
    {   
        base.Awake();
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        audioSource = GetComponent<AudioSource>();
        score = FindObjectOfType<Score>();
        TP_Player = FindObjectOfType<TP_Player>();
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            CheckCollision();
            lastPosition = tip.position;
        }
        if(collided)
        {
            StartCoroutine(WaitToDisable());
        }
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(1.3f);
        gameObject.SetActive(false);
    }

    private void CheckCollision()
    {
        collided = true;
        if (Physics.Linecast(lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out Rigidbody body))
            {
                if (body.TryGetComponent<Enemy1>(out Enemy1 enemy) && !TP_Player.GetGameOver())
                {
                    collidedWithEnemy = true;
                    score.UpdateScore();
                    enemy.SpawnExplosionEffect();
                    Destroy(enemy.gameObject);
                    audioSource.clip = hitEnemyClip;
                    audioSource.Play();
                    spawner.IncreaseDestroyedEnemiesAmount();
                }
            }
            Stop();
        }
    }
    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
        
        if(!collidedWithEnemy)
        {
            audioSource.clip = hitClip;
            audioSource.Play();
        }
    }

    public void Release(float value)
    {
        inAir = true;
        SetPhysics(true);
        MaskAndFire(value);
        StartCoroutine(RotateWithVelocity());

        lastPosition = tip.position;

        audioSource.clip = launchClip;
        audioSource.Play();
    }

    private void SetPhysics(bool usePhysics)
    {
        rb.useGravity = usePhysics;
        rb.isKinematic = !usePhysics;
    }

    private void MaskAndFire(float power)
    {
        colliders[0].enabled = false;
        interactionLayerMask = 1 << LayerMask.NameToLayer("Ignore");
        Vector3 force = transform.forward * power * speed;
        rb.AddForce(force, ForceMode.Impulse);
    }
    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rb.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    public   void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
    }

    public void ArrowHaptic(XRBaseInteractor interactor)
    {
        if (interactor is HandInteractor hand)
        {
            if (hand.TryGetComponent(out XRController controller))
                HapticManager.Impulse(.7f, .05f, controller.inputDevice);
        }
    }
}
