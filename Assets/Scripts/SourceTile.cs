using UnityEngine;
using System.Collections;


public class SourceTile : AbstractTile {

    public void Initialize(uint x, uint y, TileMap map)
    {
        base.Initialize(x, y, map, 0, float.MaxValue);
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