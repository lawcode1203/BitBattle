using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashController : MonoBehaviour
{
    public float lifeTime = 0.5f;

    public GameObject owner;
    public bool left;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        setFacing();
        Destroy(gameObject, lifeTime);
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "replicator") {
            Destroy(other);
        }
    }

    void setFacing(){
        if (left){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else{
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
