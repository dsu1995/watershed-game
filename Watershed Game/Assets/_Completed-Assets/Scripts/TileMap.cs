using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap {

    AbstractTile[,] tiles;

    uint width, height;

	public TileMap(uint width, uint height)
    {
        this.width = width+2;
        this.height = height+2;
        tiles = new AbstractTile[width, height];

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
				if (i == 0 || i == width - 1 || j == 0 || j == height - 1) {
                    AbstractTile tile = Random.Range(0, 2) == 0 ? (AbstractTile)(new SinkTile(i, j, this)) : (AbstractTile)(new SourceTile(i, j, this));
                    tiles[i, j] = tile;
				} else {
					tiles [i, j] = new GrassTile (i, j, this, Random.Range (0, 1000));
				}
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
