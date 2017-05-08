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
                if (hit.collider.tag == "Split")
                {
                    lineRenderer.SetPosition(i + 1, hit.collider.transform.position);
                } else
                {
                    lineRenderer.SetPosition(i + 1, hit.point);
                }

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
                } else if (hit.collider.tag == "Split")
                {
                    SplitLaser splitter = hit.collider.gameObject.GetComponent<SplitLaser>();
                    splitter.isHit = true;
                    currentFoward =  Quaternion.Euler(0, -45, -90) * hit.collider.transform.forward;
                    currentHit = hit.collider.transform.position;
                    splitter.currentFoward = new Vector3(-1, 0, 1);
                    splitter.currentHit = hit.collider.transform.position;
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