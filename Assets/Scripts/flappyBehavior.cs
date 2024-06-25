using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class flappyBehavior : MonoBehaviour
{
    public int health = 2;
    public bool movingUp = true;

    public float moveSpeed = 2f;
    public float height = 6f;

    public float distanceToAttack = 14.0f;

    private Vector3 startpos;
    public float attackForce = 5f;

    private float timeSinceLastAttack = 0f;
    float timeDelayBetweenAttacks = 3f;

    void Start()
    {
        startpos = transform.position; // Initialize start position in Start method
    }

    void hover()
    {
        // Determine if should switch direction
        movingUp = shouldBeMovingUp();

        // Move up or down based on movingUp state
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    bool shouldBeMovingUp()
    {
        // If the object is above the upper limit, it should move down
        if (transform.position.y >= startpos.y + height)
        {
            return false;
        }
        // If the object is below the lower limit, it should move up
        else if (transform.position.y <= startpos.y - height)
        {
            return true;
        }
        // Maintain the current direction if within bounds
        return movingUp;
    }

    bool shouldAttack(){
        // If the player is near this flappy, return true, else false
        bool playerNear = (Vector3.Distance(transform.position, getPlayerByTag("Player").transform.position) < distanceToAttack);
        bool timePassed = (Time.time - timeSinceLastAttack > timeDelayBetweenAttacks);
        return playerNear && timePassed;

    }

    void Update()
    {
        checkDeath();
        hover();

        if (shouldAttack()){
            flappyAttack();
            timeSinceLastAttack = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "slash"){
            health--;
        }
    }

    void checkDeath()
    {
        if (health <= 0)
        {
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

    void flappyAttack(){
        // Instantiate the projectile prefab and apply a force pointing to the player
        GameObject projectile = (GameObject)Instantiate(Resources.Load("Prefabs/projectile"), transform.position, Quaternion.identity);
        GameObject player = getPlayerByTag("Player");
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce((player.transform.position - projectile.transform.position) * attackForce, ForceMode2D.Impulse);
    }

}
