using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creeperBehavior : MonoBehaviour
{
    public int health = 1;
    private Rigidbody2D rb;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 2){
            move();
        }
        
    }


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "acid") {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<playerController>().TakeDamage();
            Destroy(gameObject);
        }

        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "slash") {
            takeDamage();
        }
    }

    void takeDamage(){
        health--;
        if (health <= 0){
            Destroy(gameObject);
        }
    }

    GameObject getPlayerByTag(string tag){
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);
        if (players.Length > 0){
            return players[0];
        }
        return null;
    }

    bool shouldGoLeft(){
        GameObject player = getPlayerByTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 direction = playerPos - myPos;
        direction = direction.normalized;
        return direction.x < 0;
    }

    public void move(){
        if (shouldGoLeft() && isGrounded()){
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
        else if (!shouldGoLeft() && isGrounded()){
            rb.velocity = new Vector2(moveSpeed, 0);
        }
    }

    bool isGrounded() {
        float circleRadius = 1f;
        return Physics2D.OverlapCircle(transform.position, circleRadius, LayerMask.GetMask("Ground"));
    }
}
