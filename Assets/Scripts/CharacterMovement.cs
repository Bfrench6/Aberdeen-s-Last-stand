//based off player movement from M0
//Ben French, Chuan Yui, Pranav Bhardwaj


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    
    public Rigidbody playerRigidbody;                   // reference to player rigidbody

    private Animator anim;                              // reference to the animator on the character
    private AnimatorStateInfo currentBaseState;			// reference to the current state of the animator, used for base layer

    static int walkState = Animator.StringToHash("Base Layer.Walking");
    static int drawState = Animator.StringToHash("Base Layer.Draw Arrow");
    static int runState = Animator.StringToHash("Base Layer.Running");
    static int aimState = Animator.StringToHash("Base Layer.Aiming");
    static int shootState = Animator.StringToHash("Base Layer.Shoot");
    static int idleState = Animator.StringToHash("Base Layer.Idle");

    float camRayLength = 100f;
    int floorMask;
    
    
    private bool isRunning;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!Manager.Instance.isPaused && !Manager.Instance.gameOver)
        {
           
            float h = Input.GetAxis("Horizontal");              // setup h variable as our horizontal input axis
            float v = Input.GetAxis("Vertical");                // setup v variables as our vertical input axis

            Vector2 moveVec = new Vector2(h, v);
            //the vector the camera is facing
            Vector2 lookVec = new Vector2(transform.forward.x, transform.forward.z);
            lookVec = Quaternion.Euler(0, 0, 220) * lookVec;
            lookVec.Normalize();

            Vector2 forwardVec = new Vector2(0, 1);

            float rotAngle = Vector2.Angle(lookVec, forwardVec);
            
            if (lookVec.x < 0)
            {
                rotAngle = -rotAngle;
            }
            //adjust movement vector based on the rotation the camera is facing
            moveVec = Quaternion.Euler(0, 0, rotAngle) * moveVec;
            moveVec.Normalize();

            anim.SetFloat("Speed", moveVec.y);                          // set our animator's float parameter 'Speed' equal to the vertical input axis				
            anim.SetFloat("Direction", moveVec.x);                      // set our animator's float parameter 'Direction' equal to the horizontal input axis

            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

            if (h != 0 || v != 0)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            if (currentBaseState.fullPathHash != runState)
            {
                isRunning = false;
            }

            if (!(currentBaseState.fullPathHash == drawState || currentBaseState.fullPathHash == aimState))
            {
                isRunning = true;
            }

            Turning();
        }
       
    }

    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();

        if (animator && !Manager.Instance.gameOver && !Manager.Instance.isPaused)
        {
            Vector3 newPosition = transform.position;
            float runSpeed = Input.GetAxis("Vertical");
            float sideSpeed = Input.GetAxis("Horizontal");

            Vector2 moveVec = new Vector2(runSpeed, sideSpeed);
            moveVec.Normalize();

            //adjust movement vector based on camera
            moveVec = Quaternion.Euler(0, 0, 220) * moveVec;

            if (isRunning)
            {
                moveVec *= Manager.Instance.movePU ? 5 * 1.5f : 5;
            }
            else
            {
                moveVec *= Manager.Instance.movePU ? 3 * 1.5f : 3;
            }

            newPosition.z += moveVec.x * Time.deltaTime;
            newPosition.x += moveVec.y * Time.deltaTime;

            //bound character movement within town
            if (newPosition.x < -55)
            {
                newPosition.x = -55;
            } else if (newPosition.x > 45)
            {
                newPosition.x = 45;
            }
            if (newPosition.z > 40)
            {
                newPosition.z = 40;
            } else if (newPosition.z < -60)
            {
                newPosition.z = -60;
            }
            transform.position = newPosition;
        }
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
