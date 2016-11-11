using UnityEngine;
using System.Collections;


public class SinkTile : AbstractTile {

	public SinkTile(uint x, uint y, TileMap map)
		: base(x, y, map, float.MinValue)
	{ }

	public override float getPermeability()
	{
		return 1;
	}

	public override string getType()
	{
		return "sink";
	}
}
