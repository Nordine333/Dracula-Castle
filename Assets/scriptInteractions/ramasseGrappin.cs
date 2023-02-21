using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ramasseGrappin : MonoBehaviour
{
	public GameObject player;
	public GameObject grappinJoueur;
	
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
		if(collision.transform.tag == "Player")
		{
			player.GetComponent<grappin>().enabled = true;
			Destroy(gameObject);
			grappinJoueur.SetActive(true);
		}
		
	}
}
