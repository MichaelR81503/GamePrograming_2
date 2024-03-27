using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public GameObject myPlayer;
    public Vector3 enemyDir = new Vector3(0,0,0);
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 PlayerPos = myPlayer.transform.position;
       // Vector3 TargetPos = (PlayerPos - transform.position).normalized;
       // transform.Translate(TargetPos * speed);

        distance =  Vector2.Distance(transform.position, myPlayer.transform.position);
        Vector2 direction= myPlayer.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, myPlayer.transform.position, speed * Time.deltaTime); // allows the enemy to follow the player
        transform.rotation = Quaternion.Euler(Vector3.forward * angle); 
       
    }
}
