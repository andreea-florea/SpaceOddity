using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildControlScript : MonoBehaviour {

	public ControlScript ParentScript;

	public void OnMouseOver() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			ParentScript.TriggerLeftButtonAction();
		}
		if (Input.GetMouseButtonDown(1))
		{
			ParentScript.TriggerRightButtonAction();
		}
	}
}
