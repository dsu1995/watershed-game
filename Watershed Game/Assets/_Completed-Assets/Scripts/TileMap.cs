using UnityEngine;
using System.Collections;


public class TileMap {

    AbstractTile[,] tiles;

    uint width, height;

	public TileMap(uint width, uint height)
    {
        this.width = width;
        this.height = height;
        tiles = new AbstractTile[width, height];

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                tiles[i,j] = new GrassTile(i, j, this, Random.Range(0, 1000));
            }
        }
    }
}
