using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Vars")]
    public float speed = 10f;
    public float lookSpeed = 100f;
    public GameObject myBall;
    Rigidbody ballrb;

    [Header("Jump Vars")]
    public float jumpForce = 50f;
    public bool canJump;
    public bool jumped;

    [Header("Kick Vars")]
    public Transform myFoot;
    public float kickForce = 50f;
    public float upForce = 10f;
    public float legLength = 5f;


    Rigidbody myRB;
    public float camLock; //maxlook up/down

    Vector3 myLook;
    Vector3 lookDiff;
    float onStartTimer;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lookDiff = Vector3.zero;
    }


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        canJump = true;
        jumped = false;
        myLook = transform.localEulerAngles;
        onStartTimer = 0f;
        //get the current mouse position
        //zero out our rotations based off that value

        ballrb = myBall.GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {
        onStartTimer += Time.fixedDeltaTime;
        //camera forward direction
        myLook += lookDiff;

        //clamp the magnitude to keep the player from looking fully upside down
        myLook.y = Mathf.Clamp(myLook.y, -camLock, camLock);


        Debug.Log("current myLook: " + myLook);
        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);

        //check for key and ability to jump (canJump boolean)
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            jumped = true;
        }
        else { jumped = false; }

        if (Input.GetKey(KeyCode.Return))
        {
            Kick();
        }

        lookDiff = DeltaLook() * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector3 pMove = transform.TransformDirection(Dir());
        myRB.AddForce(pMove * speed * Time.fixedDeltaTime);

        //player raw input - in magenta
        //Debug.DrawRay(transform.position, pMove * 5f, Color.magenta);
        //Debug.DrawRay(transform.position, Vector3.up, Color.magenta);

        //combined velocity of the rigidbody in black
        //Debug.DrawRay(transform.position + Vector3.up, myRB.velocity.normalized*5f, Color.black);

        if (jumped && canJump)
        {
            Jump();
        }
    }

    Vector3 Dir()
    {
        //reference Unity Input Manager virtual axes here
        //horizontal and vertical for WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 myDir = new Vector3(x, 0, z);

        //remove console clutter by only logging direction when input is pressed
        if (myDir != Vector3.zero)
        {
            Debug.Log("Player Move Dir: " + myDir);
        }

        return myDir;
    }

    Vector3 DeltaLook()
    {
        Vector3 dLook;
        float rotY = Input.GetAxisRaw("Mouse Y") * lookSpeed;
        float rotX = Input.GetAxisRaw("Mouse X") * lookSpeed;
        dLook = new Vector3(rotX, rotY, 0);

        if (dLook != Vector3.zero)
        {
            //Debug.Log("delta look: " + dLook);
        }

        if (onStartTimer < 1f)
        {
            dLook = Vector3.ClampMagnitude(dLook, onStartTimer * 10f);
        }

        return dLook;
    }

    //add a jumpForce and flip boolean for jump request (jumped) to false
    void Jump()
    {
        myRB.AddForce(Vector3.up * jumpForce);
        jumped = false;
    }

    void Kick()
    {

        //boolean tha turns ball kinematic flag off
        ballrb.isKinematic = false;
        ballrb.AddForce(transform.forward * 50f);
        //turn off the ability to kick additional times after the first
        //boolean to flag ball as kicked
        //may want to turn off player move? test for issues
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") { canJump = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") { canJump = false; }
    }

}