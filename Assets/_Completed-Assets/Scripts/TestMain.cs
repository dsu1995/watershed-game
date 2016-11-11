using System;
using UnityEngine;

public class TestMain : MonoBehaviour
{
	void Start(){
		TileLoader.loadTileMapFromFile("test");
	}
}
