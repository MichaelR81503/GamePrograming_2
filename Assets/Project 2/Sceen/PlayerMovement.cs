using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    Rigidbody myRB;
    public Camera myCam;

    Vector3 myLook;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myLook = myCam.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerlook = myCam.transform.forward;
        Debug.DrawRay(transform.position, playerlook, Color.white);
        Vector3 newlook = DeltaLook();

        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);
    }

    void FixedUpdate()
    {
        Vector3 pMove = Dir();
        myRB.AddForce(Dir() * speed * Time.fixedDeltaTime);
        Debug.DrawRay(transform.position, pMove * 5f, Color.magenta);
        Debug.DrawRay(transform.position, myRB.velocity.normalized * 5f, Color.black);
    }

    Vector3 Dir()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 myDir = new Vector3(x, 0, z);

        if (myDir != Vector3.zero)
        {
            Debug.Log("Player Move Dir: " + myDir);
        }


        return myDir;
    }


    Vector3 DeltaLook();
   {
        Vector3 dLook = Vector3.zero;
    float rotY = Input.GetAxisRaw("Mouse Y") * lookSpeed;
    float rotX = Input.GetAxisRaw("Mouse X") * lookSpeed;
}

}

