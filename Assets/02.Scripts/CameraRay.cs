using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    private ARTapTopPlaceObject onoffPortFolio;

    private Transform tr;
    private Ray cameraRay;
    private RaycastHit hit;

    //hit objects
    private GameObject hitAlive;
    private GameObject hitPenguin;
    private Transform penguinTr;

    private Camera arCamera;

    //AliveCube material
    private Material alivecubeMat;

    void Start()
    {
        tr = this.transform;
        //hitPenguin = GameObject.FindWithTag("PENGUIN");

        //hitAlive = GameObject.FindWithTag("ALIVECUBE");
    }


    void Update()
    {
        hitAlive = GameObject.FindWithTag("ALIVECUBE");
        hitPenguin = GameObject.FindWithTag("PENGUIN");
   


        cameraRay = new Ray(tr.position, tr.forward);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction*10.0f, Color.red);
 
            AliveCubeAni();
            PenguinAni();


    }
    void PenguinAni()
    {
        if (Physics.Raycast(cameraRay, out hit, 100.0f, 1 << 12))
        {
            //애니메이션(무한회전)
            hitPenguin.gameObject.GetComponent<Animator>().SetBool("JUMP", true);
            hitPenguin.transform.LookAt(tr);
        }
        else
        {
            hitPenguin.gameObject.GetComponent<Animator>().SetBool("JUMP", false);
            hitPenguin.transform.rotation = Quaternion.Euler(0,180,0);
        }
    }



    void AliveCubeAni()
    {

        if (Physics.Raycast(cameraRay, out hit, 100.0f, 1 << 11))
        {
            //애니메이션(무한회전)
            hitAlive.gameObject.GetComponent<Animator>().SetBool("ISLOOK", true);

            //AliveCube설명 UI 활성화
            hitAlive.transform.GetChild(1).gameObject.SetActive(true);
            //Glow 효과
            alivecubeMat = hitAlive.gameObject.GetComponent<MeshRenderer>().material;
            alivecubeMat.SetVector("_GLOWCOLOR", new Color(0f, 105f, 190f, 0f) * 0.1f);
        }
        else
        {

            hitAlive.gameObject.GetComponent<Animator>().SetBool("ISLOOK", false);

            //AliveCube설명 UI 활성화
            hitAlive.transform.GetChild(1).gameObject.SetActive(false);

            alivecubeMat = hitAlive.gameObject.GetComponent<MeshRenderer>().material;
            alivecubeMat.SetVector("_GLOWCOLOR", new Color(0f, 105f, 190f, 0f) * 0.01f);

        }
    }

}
