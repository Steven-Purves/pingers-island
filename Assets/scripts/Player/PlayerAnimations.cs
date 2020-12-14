using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator myAnimator;

    public void AnimateWalk(float forward, float side)
    {
        myAnimator.SetFloat("Forward", forward);
        myAnimator.SetFloat("Side", side); ;
    }
}
