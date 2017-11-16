using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewInterface;

public class UnityRenderable : IRenderable 
{
	private GameObject gameObject;
	private float topPosition;

	public IAction LeftClickAction { get; set; }
	public IAction RightClickAction { get; set; }

	public UnityRenderable(GameObject gameObject, float topPosition)
	{
		this.gameObject = gameObject;
		this.topPosition = topPosition;
	}


	public void Update(Geometry.Vector2 position, Geometry.Vector2 rotation, Geometry.Vector2 scale)
	{
		gameObject.transform.position = new Vector3((float)position.X, topPosition, (float)position.Y);

		gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, 
			180 - (float)rotation.Angle * 180 / Mathf.PI, 
			gameObject.transform.eulerAngles.z);
		
		gameObject.transform.localScale = new Vector3((float)scale.X, (float)(scale.X + scale.Y) * 0.5f, (float)scale.Y);
	}

	public void Delete()
	{
		GameObject.Destroy(gameObject);
	}
}
