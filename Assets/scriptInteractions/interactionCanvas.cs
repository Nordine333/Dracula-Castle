using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionCanvas : MonoBehaviour
{
	public GameObject canvasAAfficher;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider collision)
	{
		if(collision.transform.tag == "Player") {canvasAAfficher.SetActive(true);}
		
	}
	void OnTriggerExit(Collider collision)
	{
		if(collision.transform.tag == "Player") {canvasAAfficher.SetActive(false);}
		
	}
	

}
