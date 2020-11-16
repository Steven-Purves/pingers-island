using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour {

	public virtual void OnObjReuse() { }
	protected void Destroy()
	{
		gameObject.SetActive(false);
	}
}
