using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewInterface;

public class PrefabRenderableFactory : IRenderableFactory 
{
	private GameObject prefab;
	private float topPosition;

	public PrefabRenderableFactory(GameObject prefab, float topPosition)
	{
		this.prefab = prefab;
		this.topPosition = topPosition;
	}

	public IRenderable CreateRenderable ()
	{
		var gameObject = GameObject.Instantiate(prefab);
		var worldObject = new UnityRenderable (gameObject, topPosition);

		if (gameObject.GetComponent<ControlScript>() != null)
		{
			gameObject.GetComponent<ControlScript>().ControlObject = worldObject;
		}

		return worldObject;
	}
}
