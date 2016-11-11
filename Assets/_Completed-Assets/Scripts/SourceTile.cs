using UnityEngine;
using System.Collections;


public class SourceTile : AbstractTile {

	public SourceTile(uint x, uint y, TileMap map)
		: base(x, y, map, float.MaxValue)
	{ }

	public override float getPermeability()
	{
		return 0;
	}

	public override string getType()
	{
		return "source";
	}
}
