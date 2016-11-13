using UnityEngine;
using System.Collections;

public class CommercialTile : AbstractTile {

	public override void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0)
	{
		base.Initialize(x, y, map, elevation, SurfaceWater, waterLevel);
	}

	public override float getPermeability()
	{
		return 0.5f;
	}

	public override string getType()
	{
		return "commercial";
	}
}