using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<AbstractTile> getNeighbours(AbstractTile tile) {
		List<AbstractTile> neighbours = new List<AbstractTile>();
		if (tile.x > 0) {
			neighbours.Add (tiles [tile.x - 1, tile.y]);
		}
		if (tile.y > 0) {
			neighbours.Add (tiles [tile.x, tile.y - 1]);
		}
		if (tile.x < width-1) {
			neighbours.Add (tiles [tile.x + 1, tile.y]);
		}
		if (tile.y < height-1) {
			neighbours.Add (tiles [tile.x, tile.y + 1]);
		}
		return neighbours;
    }
}
