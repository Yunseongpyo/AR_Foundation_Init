using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapTopPlaceObject : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    private void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        visual.SetActive(false);
    }

    private void Update()
    {
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if(hits.Count >0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if(!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }
        }
    }
}
