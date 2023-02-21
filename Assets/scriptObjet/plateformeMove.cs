using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeMove : MonoBehaviour
{
	public Vector3 depart;
	public Vector3 arrivee;
	public float speed = 1.0f;
	
	private bool indicateur = false;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Vector3.Distance(transform.position,arrivee) < 0.001f) {indicateur = !indicateur;}
		else if(Vector3.Distance(transform.position,depart) < 0.001f) {indicateur = !indicateur;}
		
        if(indicateur) 
        {
			transform.position = Vector3.MoveTowards(transform.position,arrivee,Time.deltaTime * speed);
		}
		else 
        {
			transform.position = Vector3.MoveTowards (transform.position,depart,Time.deltaTime * speed);
		}
    }
}
