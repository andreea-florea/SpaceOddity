using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildControlScript : MonoBehaviour {

	public ControlScript ParentScript;

	public void OnMouseDown() 
	{
		ParentScript.TriggerLeftButtonAction();
	}
}
