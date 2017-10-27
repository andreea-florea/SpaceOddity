using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstructedGame;
using ViewModel;

public class FancyMainScript : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject blockCorePrefab;
	public GameObject roundCornerPrefab;
	public GameObject straightUpConrerPrefab;
	public GameObject straightRightCornerPrefab;
	public GameObject closedCornerPrefab;
	public GameObject outsideUpCornerPrefab;
	public GameObject outsideRightCornerPrefab;
	public GameObject roundEdgePrefab;
	public GameObject closedEdgePrefab;

	void Start () 
	{
		var tileObjectFactory = new PrefabWorldObjectFactory (tilePrefab, 0);
		var blockCoreFactory = new PrefabWorldObjectFactory (blockCorePrefab, 0);
		var roundCornerFactory = new PrefabWorldObjectFactory (roundCornerPrefab, 0);
		var straightUpCornerFactory = new PrefabWorldObjectFactory (straightUpConrerPrefab, 0);
		var straightRightCornerFactory = new PrefabWorldObjectFactory (straightRightCornerPrefab, 0);
		var closedCornerFactory = new PrefabWorldObjectFactory (closedCornerPrefab, 0);
		var outsideUpCornerFactory = new PrefabWorldObjectFactory (outsideUpCornerPrefab, 0);
		var outsideRightCornerFactory = new PrefabWorldObjectFactory (outsideRightCornerPrefab, 0);
		var roundEdgeFactory = new PrefabWorldObjectFactory (roundEdgePrefab, 0);
		var closedEdgeFactory = new PrefabWorldObjectFactory (closedEdgePrefab, 0);

		var blueprintBuilderFactory = new GameViewFactory();
		blueprintBuilderFactory.CreateBlueprintBuilderView(tileObjectFactory, 
			blockCoreFactory,
			roundCornerFactory,
			straightUpCornerFactory,
			straightRightCornerFactory,
			closedCornerFactory,
			outsideUpCornerFactory,
			outsideRightCornerFactory,
			roundEdgeFactory,
			closedEdgeFactory,
			new FullRectangleSection(new Geometry.Rectangle(new Geometry.Vector2(-10, -10), new Geometry.Vector2(10, 10))));
	}
}
