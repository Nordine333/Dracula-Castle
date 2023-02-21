using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotate : MonoBehaviour
{
	public Vector3 rotation;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.localRotation.eulerAngles.x+rotation.x,transform.localRotation.eulerAngles.y+rotation.y,transform.localRotation.eulerAngles.z+rotation.z);
    }
}
