using UnityEngine;
using System.Collections;

public class GrassTile : AbstractTile {

    private static float DEFAULT_WATER_THRESHOLD_LEVEL = 20f;

    public void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0)
    {
        base.Initialize(x, y, map, elevation, SurfaceWater, waterLevel, DEFAULT_WATER_THRESHOLD_LEVEL);
    }

    public override float getPermeability()
    {
        return 0.5f;
    }

    public override string getType()
    {
        return "grass";
    }
}
