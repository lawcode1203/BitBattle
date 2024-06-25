using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabusController : MonoBehaviour
{
    private bool isShowing = false;
    private Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        // move the databus off the screen
        currentPosition = gameObject.transform.position;
        gameObject.transform.position = new Vector3(-100, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void ShowDataBus(){
        //Debug.Log("Showing databus");
        gameObject.transform.position = currentPosition;
        isShowing = true;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("Databus collided with " + other.gameObject.name);
        if (other.gameObject.tag == "Player" && isShowing) {
            nextLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("Databus triggered with " + other.gameObject.name);
        if (other.gameObject.tag == "Player" && isShowing) {
            nextLevel();
        }
    }

    void nextLevel(){
        // Load a scene (Scene 2)
        Debug.Log("Next level");
        SceneManager.LoadScene("Level2");
    }
}
