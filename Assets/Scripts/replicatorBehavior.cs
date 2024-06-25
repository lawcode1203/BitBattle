using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replicatorBehavior : MonoBehaviour
{
    public int health = 1;

    public int maxEnemies = 5;
    private int currentEnemies = 0;

    [Header("Probabilities")]
    public float probabilityOfCreeper = 0.5f;
    public float probabilityOfHoppy = 0.3f;
    public float probabilityOfFlappy = 0.2f;

    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemies < maxEnemies){
            spawnEntities();
        }

        currentEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        
    }

    void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "slash") {
                AnnounceDeath();
                // Pause for 1 second before destroying the object

                Destroy(gameObject, 1f);
        }
    }

    void AnnounceDeath() {
        // Change the color of the replicator to blue
        GetComponent<Renderer>().material.color = Color.blue;

        // Access the game controller script
        gameControllerBehavior controller = GameObject.Find("GameController").GetComponent<gameControllerBehavior>();

        // Call the ReplicatorDeath method in the game controller script
        if (!dead){
            controller.ReplicatorDeath();
            dead = true;
        }

    }

    void spawnEntities() {
        int choice = Random.Range(0, 100);

        if (choice < 100 * probabilityOfCreeper){
            spawnCreeper();
        }
        if (choice < 100 * probabilityOfHoppy){
            spawnHoppy();
        }
        if (choice < 100 * probabilityOfFlappy){
            spawnFlappy();
        }
    }

    Vector3 getRandomVector(float scale){
        float x = Random.Range(-scale, scale);
        float y = Random.Range(-scale, scale);
        float z = 0f;
        return new Vector3(x, y, z);
    }

    void spawnCreeper() {
        GameObject creeper = GameObject.Instantiate(Resources.Load("Prefabs/Creeper")) as GameObject;
        creeper.transform.position = transform.position + getRandomVector(1f);
    }

    void spawnHoppy() {
        GameObject hoppy = GameObject.Instantiate(Resources.Load("Prefabs/hoppy")) as GameObject;
        hoppy.transform.position = transform.position + getRandomVector(1f);
    }

    void spawnFlappy() {
        GameObject flappy = GameObject.Instantiate(Resources.Load("Prefabs/flappy")) as GameObject;
        flappy.transform.position = transform.position + getRandomVector(4f);
    }
}
