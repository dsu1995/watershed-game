﻿using UnityEngine;
using System.Collections;


public class SourceTile : AbstractTile {

	public void Initialize(uint x, uint y, TileMap map)
    {
        base.Initialize(x, y, map, float.MaxValue, float.MaxValue);
    }

	public override float getPermeability()
	{
		return 0;
	}

	public override string getType()
	{
		return "source";
	}

    protected override void sendWater(float amountSent) { }
}
