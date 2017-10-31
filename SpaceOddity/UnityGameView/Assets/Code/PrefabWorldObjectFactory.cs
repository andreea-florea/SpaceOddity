using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewInterface;

public class PrefabWorldObjectFactory : IWorldObjectFactory
{
	private GameObject prefab;
	private float topPosition;

	public PrefabWorldObjectFactory(GameObject prefab, float topPosition)
	{
		this.prefab = prefab;
		this.topPosition = topPosition;
	}

	public IWorldObject CreateObject()
	{
		var gameObject = GameObject.Instantiate(prefab);
		var worldObject = new UnityWorldObject (gameObject, topPosition, 
			new Geometry.Vector2 (), new Geometry.Vector2 (), new NoAction(), new NoAction());

		if (gameObject.GetComponent<ControlScript>() != null) 
		{
			gameObject.GetComponent<ControlScript>().ControlObject = worldObject;
		}

		return worldObject;
	}
}
