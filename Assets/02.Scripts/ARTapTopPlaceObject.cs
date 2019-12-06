using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapTopPlaceObject : MonoBehaviour
{


    private ARRaycastManager rayManager;
    private GameObject visual;
    private List<ARRaycastHit> hits;
    private GameObject instanceObj;


    private Ray ray;
    private RaycastHit hitobj;


    public GameObject objectToSpwan;

    public bool ischeckClear;




    public Camera arCamera;
    private void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();

        visual = transform.GetChild(0).gameObject;
        //Touch touch = Input.GetTouch(0);
        visual.SetActive(false);
    }


   
    private void Update()
    {
        //if (Input.touchCount == 0) return;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy && ischeckClear == false)
            {
                visual.SetActive(true);
            }
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && ischeckClear == false)
            {
                ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if(Physics.Raycast(ray, out hitobj, 100.0f, 1<<9))
                {
                    instanceObj = Instantiate(objectToSpwan, hits[0].pose.position, hits[0].pose.rotation);
                    ischeckClear = !ischeckClear;
                    visual.SetActive(false);
                }

            }
        }
        if(ischeckClear == true)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 10))
                {
                    Destroy(instanceObj);
                    ischeckClear = !ischeckClear;
                    visual.SetActive(true);
                }

            }

        }

    }
}
