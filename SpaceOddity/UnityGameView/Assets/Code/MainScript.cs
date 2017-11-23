using UnityEngine;
using System.Collections;
using Game;
using Game.Interfaces;
using Geometry;
using ViewInterface;
using ViewModel;
using ConstructedGame;

public class MainScript : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject blockPrefab;
    public GameObject batteryPrefab;
	public GameObject pipeLinkPrefab;

    public void Start () 
	{
		var tileObjectFactory = new PrefabRenderableFactory (tilePrefab, 0);
		var blockObjectFactory = new PrefabRenderableFactory (blockPrefab, 0);
		var shipComponentsObjectFactory = new PrefabRenderableFactory(batteryPrefab, 0);
		var pipeLinkObjectFactory = new PrefabRenderableFactory (pipeLinkPrefab, 0);

        var blueprintBuilderFactory = new GameViewFactory();
		blueprintBuilderFactory.CreateBlueprintBuilderView(tileObjectFactory, blockObjectFactory, 
			shipComponentsObjectFactory, pipeLinkObjectFactory,
			new FullRectangleSection(new Geometry.Rectangle(new Geometry.Vector2(-5.5, -5.5), new Geometry.Vector2(5.5, 5.5))));
	}
}
