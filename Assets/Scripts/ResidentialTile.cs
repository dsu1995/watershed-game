using UnityEngine;
using System.Collections;

public class ResidentialTile : AbstractTile {

	public void Initialize(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
	{
		base.Initialize(x, y, map, elevation, waterLevel);
	}

	public override float getPermeability()
	{
		return 0.5f;
	}

	public override string getType()
	{
		return "residential";
	}
}