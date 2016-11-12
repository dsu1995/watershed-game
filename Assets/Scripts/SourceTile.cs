using UnityEngine;
using System.Collections;


public class SourceTile : AbstractTile {

    public void Initialize(uint x, uint y, TileMap map)
    {
        base.Initialize(x, y, map, 0, float.MaxValue, float.MaxValue);
    }

    public override void Initialize(uint x, uint y, TileMap map, float elevation, float waterLevel = 0, float waterThresholdLevel = 0) {
        // Override for dynamic cast purposes
        Initialize(x, y, map);
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

}
