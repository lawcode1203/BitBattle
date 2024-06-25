using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoppyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody2D rb;
    public float jumpForce = 5f;
    public float lungeForce = 5f;
    private float lastJumpTime = 0f;
    public float timeBetweenJumps = 3f;
    public float probabilityOfJump = 0.5f;

    public int health = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(getTimeSinceLastJump() > timeBetweenJumps){
            if (Random.Range(0, 100) >= 100 * probabilityOfJump){
                jump();
                lastJumpTime = Time.time;
            }
            
        }
        
    }

    void jump(){
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Vector3 playerDirection = getDirectionOfPlayer();
        rb.AddForce(playerDirection * lungeForce, ForceMode2D.Impulse);
    }

    Vector3 getDirectionOfPlayer(){
        GameObject player = getPlayerByTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 direction = playerPos - myPos;
        direction = direction.normalized;
        return direction;
    }

    float getTimeSinceLastJump(){
        return Time.time - lastJumpTime;
    }

    GameObject getPlayerByTag(string tag){
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);
        if (players.Length > 0){
            return players[0];
        }
        return null;
    }

    void OnTriggerEnter2D(Collider2D other){
        string other_tag = other.gameObject.tag;
        if (other_tag == "slash"){
            health--;
            checkDeath();
        } 
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        
        string other_tag = other.gameObject.tag;
        if (other_tag == "Player"){
            other.gameObject.GetComponent<playerController>().TakeDamage();
            Destroy(gameObject);
        }

        if (other_tag == "acid"){
            Debug.Log("Hoppy collided with acid");
            health = 0;
            checkDeath();
        }
    }

    void checkDeath(){
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
