using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	Animator animator;

	static readonly int popIn = Animator.StringToHash("In");

	void Start()
	{
		animator = GetComponent<Animator>();
	}
	public void AnimateHeartIn(bool whichWay)
	{
		animator.SetBool(popIn, whichWay);
	}
}
