using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLaser : MonoBehaviour {
    
	private string m_ShotButton;                // The input axis that is used for launching shells.
	private bool m_Shot;                       // Whether or not the shell has been launched with this button press.
    private float distance;
    private float counter;
    private Vector3 rotation;

    private Vector3 pointA;
    private Vector3 pointB;
    public float dest;
    private Vector3 gunLocation;

    private LineRenderer lineRenderer;
    public Transform origin;
    //private Transform destination;
    public float LineDrawSpeed = 6f;

    private void OnEnable()
	{
		
	}


	private void Start ()
	{
		// The Shot axis is based on the player number.
		m_ShotButton = "Fire1";
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        gunLocation = new Vector3(origin.position.x, origin.position.y + 1, origin.position.z);
        lineRenderer.SetPosition(0, gunLocation);
        lineRenderer.SetWidth(.45f, .45f);

        pointA = origin.position;
        pointB = origin.position + transform.forward*dest;
        //pointB.Set(pointB.x, origin.position.y, pointB.z);
        distance = Vector3.Distance(pointA,pointB);


        /*
        rotation = origin.eulerAngles;
        destination.position = origin.position + (10 * rotation);
        */
    }


	private void Update ()
	{
        lineRenderer.SetPosition(0, gunLocation);
        pointA = origin.position;
        pointB = origin.position + transform.forward * dest;
        //pointB.Set(pointB.x, origin.position.y, pointB.z);
        distance = Vector3.Distance(pointA, pointB);

        //if (counter < distance)
     //   {

            counter += .1f / LineDrawSpeed;
            float x = Mathf.Lerp(0, distance, counter);

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRenderer.SetPosition(1, pointAlongLine);
      //  }
    }

}
