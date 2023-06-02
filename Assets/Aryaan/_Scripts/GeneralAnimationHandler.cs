using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationHandler : MonoBehaviour
{

    private Animation Anim;

     bool CurrentAnimationState;

     public string AnimationName;

     // Start is called before the first frame update

     void Start()

     {

         Anim = GetComponent<Animation>();

        CurrentAnimationState = false;

    }

     public void PlayAnimationClip()

     {

         if (CurrentAnimationState == false)

         {

         Anim[AnimationName].speed = 1;

         Anim.Play();

         CurrentAnimationState = true;

    
     }

        //if we are using reverseanimationclip then disable the else if becs it is the same code
        // else if (CurrentAnimationState == true)

        // {

        //   Anim[AnimationName].speed = -1;

        //  Anim[AnimationName].time = Anim[AnimationName].length;

        //  Anim.Play();

        //  CurrentAnimationState = false;

        //  }

     }

     public void ReverseAnimationClip()

     {

         if (CurrentAnimationState == true)

         {

            Anim[AnimationName].speed = -1;

             Anim[AnimationName].time = Anim[AnimationName].length;

             Anim.Play();

             CurrentAnimationState = false;

        }

 }
 
    // void Update(){
    //     if(Input.GetKeyDown(KeyCode.K)){
    //         PlayAnimationClip();
    //     }
    // }
}
