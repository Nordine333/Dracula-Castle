using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class move : MonoBehaviour
{
	public float speed,jumpForce;
	private float AxeX;
	private float AxeY;
	
	private Rigidbody rb;
	private bool grounded = true;
	private bool saut = false;
	
	private bool bulle = false;
		
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // A implementer dans le futur
    
    /* 
    void GestionBulle()
    {
		bulle = !bulle;
		if(!bulle) {speed = 5; jumpForce = 400; } // param base
		else {speed = 8; jumpForce = 650; } // param bulle
	}*/

    // Update is called once per frame
    void Update()
    {	
		transform.Translate(AxeX * Vector3.right * speed * Time.deltaTime);
		transform.Translate(AxeY * Vector3.forward * speed * Time.deltaTime);
		
		if(saut)
		{
			saut = false;
			rb.AddForce(Vector3.up * jumpForce);
		}
		
    }
    
    private void fixedUpdate()
    {
		if(saut)
		{
			saut = false;
			rb.AddForce(Vector3.up * jumpForce);
		}
	}
	
	public void OnHorizontal(InputValue val)
	{
		AxeX = val.Get<float>();
	}
	
	public void OnVertical(InputValue val)
	{
		AxeY = val.Get<float>();
	}
	
    public void OnJump()
    {
		if(grounded) saut = true;
	}
	
	// A implementer dans le futur
	/*
	public void OnBulle()
	{
		GestionBulle();
	}*/
	
	public void GroundedSet(bool newEtat)
    {
		grounded = newEtat;
	}
	
}
