﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap {

    AbstractTile[,] tiles;
    public const float SIDE_LENGTH = 0.1f;
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

    public void draw(Mesh mesh)
    {
        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        Color32[] colors = new Color32[vertices.Length];

        uint index = 0;
        for (uint i = 0; i < width + 1; i++)
        {
            for (uint j = 0; j < height + 1; j++)
            {
                colors[index] = getColor();
                vertices[index++] = new Vector3(i * SIDE_LENGTH, j * SIDE_LENGTH, 0);
            }
        }

        int[] triangles = new int[3 * 2 * width * height];
        

        int triangleIndex = 0;
        int topLeftIndex = 0;
        int topRightIndex = topLeftIndex + 1;
        int bottomLeftIndex = (int) width + 1;
        int bottomRightIndex = bottomLeftIndex + 1;
        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                //Color32 color = getColor(tiles[i, j]);
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = bottomRightIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = topRightIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = topLeftIndex;

                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = topLeftIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = bottomLeftIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = bottomRightIndex;

                topLeftIndex++;
                topRightIndex++;
                bottomLeftIndex++;
                bottomRightIndex++;
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors32 = colors;
    }

    private Color32 getColor()
    {
        return Random.ColorHSV();
    }
}
