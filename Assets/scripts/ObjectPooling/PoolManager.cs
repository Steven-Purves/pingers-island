using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

	// In start() of object wanting the pool, create the pool // eg PoolManager.Instance.CreatePool(prefab, 30);

	// Then when you want it call PoolManager.Instance.ReuseObject(prefab, v3 , rot );   

	// The object you are wanting to reuse should inherit from PoolOject and have a // public override void OnObjReuse(); 

	static PoolManager _instance;

	public static PoolManager Instance{
		get{
			if(_instance == null){
				_instance = FindObjectOfType<PoolManager>();
			}
			return _instance;
		}
	}

	public void CreatePool(GameObject prefab, int poolSize)
	{
		GameObject poolHolder = new GameObject(prefab.name + " pool");
		poolHolder.transform.parent = transform;

		int poolKey = prefab.GetInstanceID();

		if (!poolDictionary.ContainsKey(poolKey))
		{
			poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
			
			for (int i = 0; i < poolSize; i++)
			{
				
				ObjectInstance newObj = new ObjectInstance (Instantiate(prefab) as GameObject);
			
				poolDictionary[poolKey].Enqueue(newObj);

				newObj.SetParent(poolHolder.transform);
			}
		}
	}
	public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int poolKey = prefab.GetInstanceID();

		if (poolDictionary.ContainsKey(poolKey))
		{
			ObjectInstance obj = poolDictionary[poolKey].Dequeue();

			poolDictionary[poolKey].Enqueue(obj);

			obj.Reuse(position, rotation);
		}
	}

	public class ObjectInstance {

		GameObject gameObject;
		Transform transform;

		bool hasPoolObjectComponent;
		PoolObject poolObjectScript;

		public ObjectInstance(GameObject objInstance)
		{
			gameObject = objInstance;
			transform = gameObject.transform;
			gameObject.SetActive(false);

			if (gameObject.GetComponent<PoolObject>())
			{
				hasPoolObjectComponent = true;
				poolObjectScript = gameObject.GetComponent<PoolObject>();
			}
		}
		public void Reuse(Vector3 position, Quaternion rotation)
		{


            if (gameObject != null)
            {
				gameObject.SetActive(false);
                gameObject.SetActive(true);
				transform.position = position;
                transform.rotation = rotation;
            }
            else
            {
                Time.timeScale = 0;
                print(gameObject);
            }

			if(hasPoolObjectComponent)
			{
				poolObjectScript.OnObjReuse();
			}
		}
		public void SetParent(Transform parent)
		{
			transform.parent = parent;
		}
	}
}
