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

    //포트폴리오 오브젝트
    public GameObject objectToSpwan;

    //클리어 체크 
    public bool ischeckClear;

    //aliveCube On/Off체크
    private bool aliveCubeVideoOnoff;

    //ar카메라 부분
    public Camera arCamera;
    private CameraRay onoffCameraRay;


    //임시 큐브맵
    public GameObject cubemap;
    private void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        onoffCameraRay = arCamera.GetComponent<CameraRay>();
        visual = transform.GetChild(0).gameObject;
        //Touch touch = Input.GetTouch(0);
        visual.SetActive(false);
    }


   
    private void Update()
    {
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
                    onoffCameraRay.enabled = true;
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
                    onoffCameraRay.enabled = false;
                    Destroy(instanceObj);

                    ischeckClear = !ischeckClear;
                    visual.SetActive(true);
                }

                // alive cube 비디오 켜기/끄기
                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 11) && aliveCubeVideoOnoff == false)
                {
                    Instantiate(cubemap, hitobj.transform.position + new Vector3(0,-0.5f,-1.0f), hitobj.transform.rotation);
                    hitobj.transform.GetChild(0).gameObject.SetActive(true);
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;
                }
                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 11) && aliveCubeVideoOnoff == true)
                {
                    hitobj.transform.GetChild(0).gameObject.SetActive(false);
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;
                }

            }

        }

    }
}
