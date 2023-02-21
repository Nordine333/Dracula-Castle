using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class grappin : MonoBehaviour
{
	
	public GameObject CanvasCommandeGrappin;
	public Animator Grappin;
	public AudioClip grappinSound;
	
	public float grappinPuissance;
	
	public Camera camera;
	
	private Rigidbody rb;
	private bool grounded = true;
	private Ray ray;
	private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position,Vector3.forward * 100);
		
		ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f)); // le viseur du grappin au centre de la camera
		
    } 
    
  
	
    void OnGrappin()
    {
		if(Physics.Raycast(ray, out hit))
		{	
			if(hit.transform.tag == "Anneau" && grounded ) // action grappin
			{
				GetComponent<AudioSource>().PlayOneShot(grappinSound);	
				Grappin.SetTrigger("GrappinTire");
				rb.AddForce(this.transform.forward * grappinPuissance);
				rb.AddForce(Vector3.up * grappinPuissance/2);
			}
		}  
		
	}
	
	void OnSouris(InputValue val)
	{
		if(Physics.Raycast(ray, out hit))
			{	
				if(hit.transform.tag == "Anneau") // action grappin
				{
					CanvasCommandeGrappin.SetActive(true);
				}
				else
				{
					CanvasCommandeGrappin.SetActive(false);
				}
			}  	
	}
	
	void OnCollisionEnter()
	{
		grounded = true;
	}
	
	void OnCollisionExit()
	{
		grounded = false;
	}

}
