using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float timeToLive = 20f;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToLive);

    }

    void Update(){
        
        

    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "acid") {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<playerController>().TakeDamage();
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "slash") {
            // Bounce off the slash (reverse velocity)
            rb.velocity = -rb.velocity;
            rb.AddForce(rb.velocity, ForceMode2D.Impulse);

        }
    }


}