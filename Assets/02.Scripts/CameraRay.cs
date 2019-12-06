using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    private Transform tr;
    private Ray cameraRay;
    private RaycastHit hit;

    private GameObject hitAlive;
    private Camera arCamera;


    void Start()
    {
        tr = this.transform;
        
        //hitAlive = GameObject.FindWithTag("ALIVECUBE");
    }


    void Update()
    {
        hitAlive = GameObject.FindWithTag("ALIVECUBE");

        cameraRay = new Ray(tr.position, tr.forward);
        //Debug.DrawRay(cameraRay.origin, cameraRay.direction*10.0f, Color.red);

        if (Physics.Raycast(cameraRay, out hit, 100.0f, 1<<11))
        {
            //Debug.Log("애니메이션실행");
            hitAlive.gameObject.GetComponent<Animator>().SetBool("ISLOOK", true);
        }
        else
        {
            hitAlive.gameObject.GetComponent<Animator>().SetBool("ISLOOK", false);

        }

    }

}
