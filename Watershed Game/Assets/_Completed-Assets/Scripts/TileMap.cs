using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap {

    AbstractTile[,] tiles;

    uint width, height;

	public TileMap(uint w, uint h)
    {
        this.width = w+2;
        this.height = h+2;
        tiles = new AbstractTile[width, height];

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
				if (i == 0 || i == width - 1 || j == 0 || j == height - 1) {
					tiles [i, j] = Random.Range (0, 2) == 0 ? (AbstractTile)new SinkTile (i, j, this) : (AbstractTile)new SourceTile (i, j, this);
				} else {
					tiles [i, j] = new GrassTile (i, j, this, Random.Range (0, 1000));
				}
            }
        }
    }
	public void setTile(AbstractTile t) {
		// x+1, y+1 since we have magical sink/source tiles along the perimeter of our tilemap
		tiles[t.x + 1, t.y + 1] = t;
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
