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
	public GameObject diagonalMissingCornerPrefab;
	public GameObject roundEdgePrefab;
	public GameObject closedEdgePrefab;

	void Start () 
	{
		var tileObjectFactory = new PrefabRenderableFactory (tilePrefab, 0);
		var blockCoreFactory = new PrefabRenderableFactory (blockCorePrefab, 0);
		var roundCornerFactory = new PrefabRenderableFactory (roundCornerPrefab, 0);
		var straightUpCornerFactory = new PrefabRenderableFactory (straightUpConrerPrefab, 0);
		var straightRightCornerFactory = new PrefabRenderableFactory (straightRightCornerPrefab, 0);
		var closedCornerFactory = new PrefabRenderableFactory (closedCornerPrefab, 0);
		var outsideUpCornerFactory = new PrefabRenderableFactory (outsideUpCornerPrefab, 0);
		var outsideRightCornerFactory = new PrefabRenderableFactory (outsideRightCornerPrefab, 0);
		var diagonalMissingCornerFactory = new PrefabRenderableFactory (diagonalMissingCornerPrefab, 0);
		var roundEdgeFactory = new PrefabRenderableFactory (roundEdgePrefab, 0);
		var closedEdgeFactory = new PrefabRenderableFactory (closedEdgePrefab, 0);

		var blueprintBuilderFactory = new GameViewFactory();
		blueprintBuilderFactory.CreateBlueprintBuilderView(tileObjectFactory, 
			blockCoreFactory,
			roundCornerFactory,
			straightUpCornerFactory,
			straightRightCornerFactory,
			closedCornerFactory,
			outsideUpCornerFactory,
			outsideRightCornerFactory,
			diagonalMissingCornerFactory,
			roundEdgeFactory,
			closedEdgeFactory,
			new FullRectangleSection(new Geometry.Rectangle(new Geometry.Vector2(-10, -10), new Geometry.Vector2(10, 10))));
	}
}
