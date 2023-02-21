using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPlateforme : MonoBehaviour
{
	public float speed = 1.0f; // plus elle est faible plus le changement est rapide
	
	private int compteurUpdate = 0;
	private int compteur = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(compteurUpdate == (int) (speed * 100))
		{
			compteur += 1;
			for(int i = 0; i <transform.childCount; i=i+1)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
			transform.GetChild(compteur%transform.childCount).gameObject.SetActive(true);
			compteurUpdate = 0;
		}
		else {compteurUpdate +=1; }
    }
}
