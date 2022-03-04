using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i<Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && animator.GetCurrentAnimatorStateInfo(0).IsName("none"))
            {
                animator.Play("Gudule_goBig");
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Began && animator.GetCurrentAnimatorStateInfo(0).IsName("Gudule_goSmall"))
            {
                animator.Play("none");
            }

            else if (Input.GetTouch(i).phase == TouchPhase.Began && animator.GetCurrentAnimatorStateInfo(0).IsName("Gudule_goBig"))
            {
                animator.Play("Gudule_goSmall");
            }
        }

    }
}
