using UnityEngine;
using System.Collections;


public class GrassTile : AbstractTile {

	public GrassTile(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
        : base(x, y, map, elevation, waterLevel)
    { }

    public override float getPermeability()
    {
        return 0.5f;
    }

    public override string getType()
    {
        return "grass";
    }
}
