using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Awake()
    {
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Laser();
    }

    void Laser()
    {
        Vector3 currentHit = transform.position;
        Vector3 currentFoward = transform.forward;
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, currentHit);
        for (int i = 0; i < 20; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentHit, currentFoward, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.point);
                lineRenderer.positionCount = i + 2;
                lineRenderer.SetPosition(i + 1, hit.point);

                if (hit.collider.tag == "Mirror")
                {
                    Vector3 pos = Vector3.Reflect(hit.point - this.transform.position, hit.normal);
                    currentFoward = pos;
                    currentFoward.Normalize();
                    currentHit = hit.point;
                } else if (hit.collider.tag == "Goal")
                {
                    GoalController goal = hit.collider.gameObject.GetComponent<GoalController>();
                    goal.IsHit = true;
                    break;
                } else
                {
                    break;
                }
            } else
            {
                lineRenderer.positionCount = i + 2;
                lineRenderer.SetPosition(i + 1, currentHit + currentFoward * 200);
                break;
            }
        }
    }
}