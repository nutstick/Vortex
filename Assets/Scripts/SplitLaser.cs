using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitLaser : MonoBehaviour
{

    private LineRenderer lineRenderer;
    [HideInInspector] public bool isHit;
    [HideInInspector] public Vector3 currentFoward;
    [HideInInspector] public Vector3 currentHit;

    private void OnEnable()
    {

    }


    private void Start()
    {
        isHit = false;
        lineRenderer = GetComponent<LineRenderer>();
        currentFoward = transform.up;
        currentHit = transform.position;
    }


    private void Update()
    {
        if (isHit)
        {
            Laser();
        }else
        {
            DelLaser();
        }
        isHit = false;
    }

    private void DelLaser()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void Laser()
    {
        //currentHit = transform.position;
        //currentFoward = transform.up;
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
                }
                else if (hit.collider.tag == "Goal")
                {
                    GoalController goal = hit.collider.gameObject.GetComponent<GoalController>();
                    goal.IsHit = true;
                    break;
                }
                else if (hit.collider.tag == "Split")
                {
                    SplitLaser splitter = hit.collider.gameObject.GetComponent<SplitLaser>();
                    splitter.isHit = true;
                    currentFoward = Quaternion.AngleAxis(-45, splitter.transform.forward) * splitter.transform.up;
                    currentHit = hit.collider.transform.position;
                    splitter.currentFoward = Quaternion.AngleAxis(45, splitter.transform.forward) * splitter.transform.up; ;
                    splitter.currentHit = hit.collider.transform.position;
                }
                else
                {
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount = i + 2;
                lineRenderer.SetPosition(i + 1, currentHit + currentFoward * 200);
                break;
            }
        }
    }

}
