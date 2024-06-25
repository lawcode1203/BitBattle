using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float leftRightMovementSpeed = 5f; //
    public float jumpForce = 300f;
    public bool left = true;

    public int maxHealth = 3;
    public int currentHealth;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
        fixFalling();

        setFacing();
        
    }

    void checkMovement() {
        // Right key (D)
        if (Input.GetKey(KeyCode.D)){
            left = false;
            transform.Translate(Vector3.right * leftRightMovementSpeed * Time.deltaTime);
        }

        // Left key (A)
        if (Input.GetKey(KeyCode.A)){
            left = true;
            transform.Translate(Vector3.left * leftRightMovementSpeed * Time.deltaTime);
        }

        // If spacebar is pressed then jump (Apply upward rigidbody2d force)
        if (Input.GetKeyDown(KeyCode.Space)){
            if (isGrounded()){
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);  
            }
            
        }

        // If left click is pressed then call Attack()
        if (Input.GetMouseButtonDown(0)){
            Attack();
        }
    }

    void Attack() {
        // On attack, summon a slash prefab just in front of the player
        if (left){
            GameObject slash = (GameObject)Instantiate(Resources.Load("Prefabs/slash"), transform.position + Vector3.left, Quaternion.identity);
            slash.GetComponent<slashController>().left = left;
            slash.GetComponent<slashController>().owner = gameObject;
        }
        else{
            GameObject slash = (GameObject)Instantiate(Resources.Load("Prefabs/slash"), transform.position + Vector3.right, Quaternion.identity);
            slash.GetComponent<slashController>().left = left;
            slash.GetComponent<slashController>().owner = gameObject;
        }

        
        
    }

    bool isGrounded() {
        float circleRadius = 1f;
        // Check for "Ground" layer collisions
        return Physics2D.OverlapCircle(transform.position, circleRadius, LayerMask.GetMask("Ground"));
    }

    void fixFalling(){
        // Make sure the player is flat forever
        transform.rotation = Quaternion.identity;
    }

    // Check if the player falls out of the world (the y < -100f)
    void checkFalling() {
        if (transform.position.y < -100f){
            Destroy(gameObject);
        }
    }

    // On collision with the acid tilemap collider, destroy the player
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "acid") {
            Respawn();
            Destroy(gameObject);
        }
    }
    
    void Respawn() {
        Instantiate(Resources.Load("Prefabs/player_0"), startPosition, Quaternion.identity);
    }

    void setFacing() {
        if (left){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else{
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void TakeDamage() {
        currentHealth--;
        playDamageAnimation();
        if (currentHealth <= 0){
            Respawn();
            Destroy(gameObject);
        }
    }

    public void playDamageAnimation() {
        // Flash red
        GetComponent<Renderer>().material.color = Color.red;
        Invoke("resetColor", 0.1f);

    }

    public void resetColor() {
        GetComponent<Renderer>().material.color = Color.white;
    }

}
