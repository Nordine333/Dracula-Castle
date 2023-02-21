using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowMouse : MonoBehaviour
{	
	[Range(0.1f,1.0f)]
	public float speed;
	
	private float mouseAngleX = 0f;
	private float mouseAngleY = 0f;

	private Vector2 sourisAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         mouseAngleX += sourisAngle.x * speed;
         mouseAngleY += sourisAngle.y * speed* 0.75f; //75% de la sensi de X
         mouseAngleY = Mathf.Clamp(mouseAngleY,-90,90);

        transform.localEulerAngles = new Vector3(-mouseAngleY,mouseAngleX,transform.localEulerAngles.z);
        
    }
    
    void OnSouris(InputValue val)
    {	
		sourisAngle = val.Get<Vector2>();
	}
}
