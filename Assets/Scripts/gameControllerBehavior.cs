using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameControllerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int replicatorsLeft = 3;
    // Get the textmeshpro component
    public TextMeshProUGUI replicatorText;
    void Start()
    {
        replicatorText.text = "Replicators Left: " + replicatorsLeft.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplicatorDeath(){
        // Pause for 2 seconds
        replicatorsLeft -= 1;

        replicatorText.text = "Replicators Left: " + replicatorsLeft.ToString();

        if (replicatorsLeft == 0){
            Debug.Log("Level Complete");
            replicatorText.text = "Level Complete!";
            startDataBus();
        }
    }

    public void startDataBus(){
        DatabusController databus = GameObject.Find("Data Bus").GetComponent<DatabusController>();
        if (databus == null){
            Debug.Log("Databus not found");
        }
        databus.ShowDataBus();
    }
}
