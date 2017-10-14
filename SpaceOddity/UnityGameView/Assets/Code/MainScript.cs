using UnityEngine;
using System.Collections;
using Game;
using Game.Interfaces;
using Geometry;
using ViewInterface;
using ViewModel;
using ViewModel.Interfaces;
using ConstructedGame;

public class MainScript : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject blockPrefab;

	public void Start () 
	{
		var tileObjectFactory = new PrefabWorldObjectFactory (tilePrefab, 0);
		var blockObjectFactory = new PrefabWorldObjectFactory (blockPrefab, 0);

		var blueprintBuilderFactory = new GameViewFactory();
		blueprintBuilderFactory.CreateBlueprintBuilderView(tileObjectFactory, blockObjectFactory, 
			new FullRectangleSection(new Geometry.Rectangle(new Geometry.Vector2(-10, -10), new Geometry.Vector2(10, 10))));
	}
}
