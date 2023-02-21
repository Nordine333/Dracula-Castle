using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGround : MonoBehaviour
{
	public GameObject Player;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag != "Player")
		{
			Player.GetComponent<move>().GroundedSet(true);
		}
	}
	
	void OnTriggerExit(Collider col) 
	{
		if(col.transform.tag != "Player") 
		{
			Player.GetComponent<move>().GroundedSet(false);
		}
	}
}
