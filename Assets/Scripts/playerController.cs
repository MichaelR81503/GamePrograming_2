using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speedX;
    public float speedY;
    private float moveX;
    private float moveY;

    public int myScore;

    public bool playerAlive = true;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive == false)
        {
            SceneManager.LoadScene(1);
            Debug.Log("Player Died");
        }
    }

    private void FixedUpdate()
    {

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveX * speedX, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, moveY * speedY);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            
            playerAlive = false;
            
        }


    }

}





