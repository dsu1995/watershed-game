using UnityEngine;
using System.Collections;


public class SinkTile : AbstractTile {
    
	public void Initialize(uint x, uint y, TileMap map, GameObject SurfaceWater)
    {
        base.Initialize(x, y, map, 0, SurfaceWater);
    }

    public override void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0, float waterThresholdLevel = 0) {
        // Override for dynamic cast purposes
        Initialize(x, y, map, 0, SurfaceWater);
    }

	public override float getPermeability()
	{
		return 1;
	}

	public override string getType()
	{
		return "sink";
	}

    public override void recieveWater(float amountRecieved) { }
    protected override void sendWater(float amountSent) { }
}
