using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractTile {

    public uint x {
        get; private set;
    }

    public uint y {
        get; private set;
    }

    public float waterLevel
    {
        get; protected set;
    }

    public float elevation
    {
        get; protected set;
    }

    TileMap map;

    public AbstractTile(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
    {
        this.x = x;
        this.y = y;
        this.map = map;
        this.waterLevel = waterLevel;
    }

    public abstract string getType();
    public abstract float getPermeability();

    public void changeWaterLevel(float delta)
    {
        waterLevel += delta;
    }

}
