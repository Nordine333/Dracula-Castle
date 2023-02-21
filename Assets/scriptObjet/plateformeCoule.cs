using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeCoule : MonoBehaviour
{
	public float speed = 1.0f;
	
	private Vector3 posDepart;
	private bool joueurCollision = false;
	
    // Start is called before the first frame update
    void Start()
    {
        posDepart = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(joueurCollision) 
        {
			Vector3 arrivee = new Vector3(posDepart.x,-9,posDepart.z);
			transform.position = Vector3.MoveTowards (transform.position,arrivee,Time.deltaTime * speed);
		}
		else 
        {
			transform.position = Vector3.MoveTowards (transform.position,posDepart,Time.deltaTime * speed);
		}
    }
    
    void OnTriggerEnter(Collider collision)
    {
		if(collision.tag == "Player")
		joueurCollision = true;
	}
	
	void OnTriggerExit(Collider collision)
    {
		if(collision.tag == "Player")
		joueurCollision = false;
	}

}
