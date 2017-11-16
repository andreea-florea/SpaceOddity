using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewInterface;

public class ControlScript : MonoBehaviour 
{
	public UnityRenderable ControlObject { get; set; }

	public void OnMouseOver() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			TriggerLeftButtonAction();
		}
		if (Input.GetMouseButtonDown(1))
		{
			TriggerRightButtonAction();
		}
	}
		
	public void TriggerLeftButtonAction()
	{
		ControlObject.LeftClickAction.Execute();
	}

	public void TriggerRightButtonAction()
	{
		ControlObject.RightClickAction.Execute();
	}
}