using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewInterface;

public class ControlScript : MonoBehaviour 
{
	public IWorldObject ControlObject { get; set; }

	public void OnMouseDown() 
	{
		TriggerLeftButtonAction();
	}
		
	public void TriggerLeftButtonAction()
	{
		ControlObject.LeftClickAction.Execute();
	}
}
