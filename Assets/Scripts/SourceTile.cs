using UnityEngine;
using System.Collections;


public class SourceTile : AbstractTile {

    private float _waterLevel; 

	public void Initialize(uint x, uint y, TileMap map, GameObject SurfaceWater)
    {
        _waterLevel = 1000f;
        base.Initialize(x, y, map, 0, SurfaceWater, _waterLevel, float.MaxValue);
    }

    public override void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0, float waterThresholdLevel = 0) {
        // Override for dynamic cast purposes
        Initialize(x, y, map, 0, SurfaceWater);
    }

	public override float getPermeability()
	{
		return 0;
	}

	public override string getType()
	{
		return "source";
	}

    public override void recieveWater(float amountRecieved) { }
    protected override void sendWater(float amountSent) { }

    public override float waterLevel
    {
        get { return _waterLevel; }
        protected set
        {
            
        }
    }

    public override float newWaterLevel
    {
        get
        {
            return _waterLevel;
        }

        protected set
        {
            
        }
    }

}
