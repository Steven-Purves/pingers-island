using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class StopLosingFocus : MonoBehaviour {

	public TMP_InputField inputField;

	void Update () 
	{
        

        if (EventSystem.current.currentSelectedGameObject == null)
		{
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
		if (Input.GetKeyDown(KeyCode.Return))
		{
			//levelManger.EnterUserName(inputField.text);

		}
        
     }
}
