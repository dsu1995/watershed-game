using UnityEngine;
using System.Collections;


public class SinkTile : AbstractTile {

    public override void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0) {
        base.Initialize(x, y, map, 0, SurfaceWater);
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

    public override float waterLevel
    {
        get { return 0; }
        protected set
        {

        }
    }

    public override float newWaterLevel
    {
        get
        {
            return 0;
        }

        protected set
        {

        }
    }
}
