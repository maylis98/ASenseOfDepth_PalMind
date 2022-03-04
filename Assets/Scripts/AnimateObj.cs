using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimateObj : MonoBehaviour
{

    Animator _Anim;
    public GameObject videoPlayer;
    public int timeToStop;


    void Start()
    {
        videoPlayer.SetActive(false);  
    }

    void Update()
    {

        /*_Anim = GetComponent<Animator>();
        _Anim.SetBool("rotate",true);*/

  
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                videoPlayer.SetActive(true);
                Destroy(videoPlayer, timeToStop);
            }
        

        /*for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && anim.GetCurrentAnimatorStateInfo(0).IsName("none"))
            {
                anim.Play("Gudule_start");
            }

            if (Input.GetTouch(i).phase == TouchPhase.Began && anim.GetCurrentAnimatorStateInfo(0).IsName("Gudule_goSmall"))
            {
                anim.Play("none");
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Began && anim.GetCurrentAnimatorStateInfo(0).IsName("Gudule_goBig"))
            {
                anim.Play("Gudule_goSmall");
            }
        }*/

    }
}
