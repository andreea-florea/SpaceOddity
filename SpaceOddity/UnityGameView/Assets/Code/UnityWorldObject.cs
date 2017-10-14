using UnityEngine;
using System.Collections;
using ViewInterface;
using Geometry;

public class UnityWorldObject : IWorldObject
{
	private GameObject gameObject;

	private float topPosition;
	private Geometry.Vector2 position;
	public Geometry.Vector2 Position 
	{
		get 
		{
			return position;
		}
		set
		{
			this.position = value;
			gameObject.transform.position = new Vector3((float)position.X, topPosition, (float)position.Y);
		}
	}

	private Geometry.Vector2 scale;
	public Geometry.Vector2 Scale 
	{ 
		get
		{
			return scale;
		}
		set
		{
			this.scale = value;
			gameObject.transform.localScale = new Vector3((float)scale.X, (float)(scale.X + scale.Y) * 0.5f, (float)scale.Y);
		}
	}

	public IAction LeftClickAction { get; set; }


	public UnityWorldObject(GameObject gameObject, float topPosition, 
		Geometry.Vector2 position, Geometry.Vector2 scale, IAction leftClickAction)
	{
		this.gameObject = gameObject;
		this.topPosition = topPosition;
		Position = position;
		Scale = scale;
		LeftClickAction = leftClickAction;
	}
}
