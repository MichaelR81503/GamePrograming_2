using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 startPosition;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
         startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         if ((transform.position - startPosition).magnitude > 50f)
         transform.position = startPosition;
    }
}
