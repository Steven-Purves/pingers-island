using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShoot : MonoBehaviour {


	public GameObject prefab;

	void Start () {
		PoolManager.Instance.CreatePool(prefab, 10);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Space))
		{
			print("space pressed");
			PoolManager.Instance.ReuseObject(prefab, transform.position, transform.rotation);
		}
	}
}
