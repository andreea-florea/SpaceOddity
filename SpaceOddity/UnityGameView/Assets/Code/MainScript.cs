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

    public void Start () 
	{
		var tileObjectFactory = new PrefabWorldObjectFactory (tilePrefab, 0);
		var blockObjectFactory = new PrefabWorldObjectFactory (blockPrefab, 0);
        var shipComponentsObjectFactory = new PrefabWorldObjectFactory(batteryPrefab, 0);

        var blueprintBuilderFactory = new GameViewFactory();
		blueprintBuilderFactory.CreateBlueprintBuilderView(tileObjectFactory, blockObjectFactory, 
            shipComponentsObjectFactory,
            new FullRectangleSection(new Geometry.Rectangle(new Geometry.Vector2(-10, -10), new Geometry.Vector2(10, 10))));
	}
}
