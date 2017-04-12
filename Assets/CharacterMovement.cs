using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public Transform RHTrans;
    public Arrow arrowPrefab;
    public Rigidbody playerRigidbody;

    private Animator anim;                              // a reference to the animator on the character
    private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer

    static int walkState = Animator.StringToHash("Base Layer.Walking");
    static int drawState = Animator.StringToHash("Base Layer.Draw Arrow");
    static int runState = Animator.StringToHash("Base Layer.Running");
    static int aimState = Animator.StringToHash("Base Layer.Aiming");
    static int shootState = Animator.StringToHash("Base Layer.Shoot");
    static int idleState = Animator.StringToHash("Base Layer.Idle");

    float camRayLength = 100f;
    int floorMask;

    private int cooldown;
    private int shotTime = 40;
    private bool isAiming;
    private bool isRunning;
    //private Arrow[] arrows = new Arrow[50];
    private Arrow arrow;
    private bool arrowKnocked;
    private bool arrowFired;

    bool isShowing = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        cooldown++;
        if (currentBaseState.fullPathHash != drawState || currentBaseState.fullPathHash != aimState)
        {
            anim.ResetTrigger("Fire");
        }
        if (Input.GetMouseButtonDown(0) && (currentBaseState.fullPathHash == runState || currentBaseState.fullPathHash == walkState || 
            currentBaseState.fullPathHash == idleState || currentBaseState.fullPathHash == shootState))
        {
            anim.SetTrigger("Aim");
            //Arrow arrow = new Arrow();
            //arrows[arrows.GetLength()] = arrow;
        }
        if (anim.GetFloat("drawArrow") > .5 && !arrowKnocked)
        {
            Debug.Log("arrow created");
            arrow = FireArrow();

            arrowKnocked = true;
        }
        //if (anim.GetFloat("drawArrow") < 0.1 && arrowKnocked)
        //{
        //    arrow.transform.rotation = Quaternion.LookRotation(transform.forward);
        //}
        if (Input.GetMouseButtonUp(0) && (currentBaseState.fullPathHash == aimState || currentBaseState.fullPathHash == drawState) )
        {
            anim.SetTrigger("Fire");
            
            arrowKnocked = false;
            arrowFired = true;
            arrow.transform.rotation = Quaternion.LookRotation(transform.forward);

            cooldown = 0;
        } else if (!Input.GetMouseButton(0) && currentBaseState.fullPathHash == aimState && cooldown > shotTime) {
            Debug.Log("Fire");
            anim.SetTrigger("Fire");
            
            arrowKnocked = false;
            arrowFired = true;
            arrow.transform.rotation = Quaternion.LookRotation(transform.forward);
            cooldown = 0;
        }

        
        


    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");              // setup h variable as our horizontal input axis
        float v = Input.GetAxis("Vertical");                // setup v variables as our vertical input axis
        anim.SetFloat("Speed", v);                          // set our animator's float parameter 'Speed' equal to the vertical input axis				
        anim.SetFloat("Direction", h);                      // set our animator's float parameter 'Direction' equal to the horizontal input axis		

        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

        if (h != 0 || v != 0)
        {
            anim.SetBool("isMoving", true);
        } else
        {
            anim.SetBool("isMoving", false);
        }
        if (currentBaseState.fullPathHash != runState)
        {
            isRunning = false;
        }
        if (currentBaseState.fullPathHash == drawState || currentBaseState.fullPathHash == aimState)
        {
            isAiming = true;

        } else
        {
            isRunning = true;
            isAiming = false;
        }

        if (anim.GetFloat("fireArrow") > 1 && arrowFired)
        {
            arrow.fired = true;
            arrow.transform.SetParent(null);
        }
        //if (!isAiming)
        //{
        //    Turning();
        //}
        Turning();
        




    }

    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();

        if (animator)
        {
            Vector3 newPosition = transform.position;
            float runSpeed = anim.GetFloat("Runspeed");
            float sideSpeed = anim.GetFloat("Sidespeed");

            Vector2 moveVec = new Vector2(runSpeed, sideSpeed);
            moveVec.Normalize();
            Vector2 forwardVec = new Vector2(transform.forward.z, transform.forward.x);
            forwardVec.Normalize();

            Debug.Log("rotation" + transform.eulerAngles);
            
            
            moveVec = Quaternion.Euler(0, 0, transform.eulerAngles.y) * moveVec;
            if (moveVec.magnitude > 0 && !isShowing)
            {
                Debug.Log("moveVec" + moveVec);
                Debug.Log("forwardVec" + forwardVec);

                isShowing = true;
                
                //cos(theta)*x-sin(theta)*y
                //sin(theta)*x+cos(theta)*y
            } else if (moveVec.magnitude == 0)
            {
                isShowing = false;
            }


            if (isRunning)
            {
                moveVec *= 4;
            }
            else
            {
                moveVec *= 2;
            }

            newPosition.z += moveVec.x * Time.deltaTime;
            newPosition.x += moveVec.y * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    Arrow FireArrow()
    {
        //Arrow arrow = Instantiate(arrowPrefab, RHTrans.position, Quaternion.LookRotation(transform.forward));
        Arrow arrow = Instantiate(arrowPrefab, RHTrans, false);

        return arrow;
        
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

}
