//Based off player attack from m0
//Ben French, Chuan Yui, Pranav Bhardwaj


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Transform RHTrans;                           //reference to the right hand of the player
    public Arrow arrowPrefab;                           //reference to arrow prefab
    public Rigidbody playerRigidbody;                   //reference to players rigidbody
    
    public AudioClip[] KnockClips;                      //reference to arrow knock sound clips
    public AudioClip[] FireClips;                       //reference to arrow fired clips

    private Animator anim;                              // a reference to the animator on the character
    private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
    
    static int drawState = Animator.StringToHash("Base Layer.Draw Arrow");
    static int runState = Animator.StringToHash("Base Layer.Running");
    static int aimState = Animator.StringToHash("Base Layer.Aiming");
    static int shootState = Animator.StringToHash("Base Layer.Shoot");
    static int idleState = Animator.StringToHash("Base Layer.Idle");

    private int cooldown;                               //arrow shot cooldown time
    private int shotTime = 45;                          //time between shots
    private bool isAiming;                              
    private bool isRunning;
    private Arrow arrow;
    private bool arrowKnocked;
    private bool arrowFired;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.Instance.isPaused)
        {
            cooldown++;

            //if not drawing or aiming, reset fire trigger
            if (currentBaseState.fullPathHash != drawState && currentBaseState.fullPathHash != aimState)
            {
                anim.ResetTrigger("Fire");
            }
            //if mouse pressed down, aim an arrow
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Aim");

            }
            //if the mouse button has been released, fire the arrow
            if (!Input.GetMouseButton(0) && (currentBaseState.fullPathHash == aimState || currentBaseState.fullPathHash == drawState) && cooldown >= shotTime)
            {
                anim.SetTrigger("Fire");
                arrowFired = true;
                //face arrow in proper direction
                if (arrow != null)
                {
                    arrow.transform.rotation = Quaternion.LookRotation(transform.forward);
                }
                cooldown = 0; //0 time since last shot
            }
        }
    }

    void FixedUpdate()
    {
        if (!Manager.Instance.isPaused || !Manager.Instance.gameOver)
        {
            //once draw animation has reached the appropriate point, create the arrow
            if (anim.GetFloat("drawArrow") > .5 && !arrowKnocked)
            {
                arrow = CreateArrow();

                arrowKnocked = true;
            }
            //fix arrows rotation prior to release
            if (anim.GetFloat("drawArrow") < 0.1 && arrowKnocked)
            {
                arrow.transform.rotation = Quaternion.LookRotation(transform.forward);
            }
        }

        currentBaseState = anim.GetCurrentAnimatorStateInfo(0); // set our currentState variable to the current state of the Base Layer (0) of animation
        //once shoot animation has reached appropriate point, fire the arrow
        if (anim.GetFloat("fireArrow") > 0.5 && arrowFired)
        {
            if (arrow != null)
            {
                arrow.Fire();
                PlayRandomSound(FireClips, transform.position);
            }
            arrowKnocked = false;
        }
    }

    Arrow CreateArrow()
    {
        Arrow arrow = Instantiate(arrowPrefab, RHTrans, false);
        return arrow;
    }

    void PlayRandomSound(AudioClip[] clips, Vector3 pos)
    {
        if (clips != null && clips.Length != 0)
        {
            AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], pos, (Manager.Instance.FXVol * Manager.Instance.masterVol));
        }
    }

    void PlayRandomKnockSound()
    {
        if (KnockClips != null && KnockClips.Length != 0)
        {
            AudioSource.PlayClipAtPoint(KnockClips[Random.Range(0, KnockClips.Length)], transform.position, (Manager.Instance.FXVol * Manager.Instance.masterVol));
        }
    }

}
