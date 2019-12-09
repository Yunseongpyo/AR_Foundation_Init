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

    private GameObject wordExplode;
    private void Awake()
    {
        onoffCameraRay = arCamera.GetComponent<CameraRay>();

    }
    private void Start()
    {
        wordExplode = Resources.Load("WordExplode") as GameObject;

        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        //Touch touch = Input.GetTouch(0);
        visual.SetActive(false);
    }

  


    private void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        //포티폴리오 생성
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

        //포트폴리오 생성중
        if(ischeckClear == true)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                

                if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 11) && aliveCubeVideoOnoff == false)
                {
                    //설명UI끄기
                    hitobj.transform.GetChild(1).gameObject.SetActive(false);

                    Instantiate(cubemap, hitobj.transform.position + new Vector3(0,-0.5f,-1.0f), hitobj.transform.rotation);
                    //카메라 레이 끄기
                    onoffCameraRay.enabled = false;
                    StartCoroutine(DelayVideoPlayer(hitobj));
                    
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;
                }
                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 11) && aliveCubeVideoOnoff == true)
                {
                    hitobj.transform.GetChild(0).gameObject.SetActive(false);

                    GameObject temp = GameObject.Find("TEMP");
                    GameObject[] cubewall = GameObject.FindGameObjectsWithTag("CUBEWALL");
                    for (int i = 0; i < cubewall.Length; i++)
                    {
                        cubewall[i].GetComponent<Rigidbody>().useGravity = true;
                        cubewall[i].GetComponent<Rigidbody>().isKinematic = false;
                    }

                    Destroy(temp, 7.0f);

                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;
                    onoffCameraRay.enabled = true;
                }

                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 15))
                {
                    Debug.Log("워드월드");
                    hitobj.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
                    Instantiate(wordExplode, hitobj.transform.position + new Vector3(0, +0.5f, 0), hitobj.transform.rotation);

                    onoffCameraRay.enabled = false;
                    //aliveCubeVideoOnoff = !aliveCubeVideoOnoff;

                }


            }

        }

    }
    
    IEnumerator DelayVideoPlayer(RaycastHit _hitobj)
    {
        yield return new WaitForSeconds(4.0f);
        _hitobj.transform.GetChild(0).gameObject.SetActive(true);

    }

}
