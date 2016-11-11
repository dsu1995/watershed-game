using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour
{

    public const float SIDE_LENGTH = 0.1f;

    public GameObject SourceTile;
    public GameObject SinkTile;
    public GameObject GrassTile;

    GameObject[,] tiles;
    uint width, height;

    public void Initialize(uint w, uint h)
    {
        this.width = w + 2;
        this.height = h + 2;
        tiles = new GameObject[width, height];

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1 || (i == 5 && j == 5))
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        tiles[i, j] = Instantiate(SourceTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        tiles[i, j].GetComponent<SourceTile>().Initialize(i, j, this);
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(SinkTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        tiles[i, j].GetComponent<SinkTile>().Initialize(i, j, this);
                    }
                }
                else
                {
                    tiles[i, j] = Instantiate(GrassTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    tiles[i, j].GetComponent<GrassTile>().Initialize(i, j, this, Random.Range(0, 1000));
                }
            }
        }
    }

    //public void setTile(AbstractTile t)
    //{
    //    // x+1, y+1 since we have magical sink/source tiles along the perimeter of our tilemap
    //    tiles[t.x + 1, t.y + 1] = t;
    //}

    public List<AbstractTile> getNeighbours(AbstractTile tile)
    {
        List<AbstractTile> neighbours = new List<AbstractTile>();
        if (tile.x > 0)
        {
            neighbours.Add(tiles[tile.x - 1, tile.y].GetComponent<AbstractTile>());
        }
        if (tile.y > 0)
        {
            neighbours.Add(tiles[tile.x, tile.y - 1].GetComponent<AbstractTile>());
        }
        if (tile.x < width - 1)
        {
            neighbours.Add(tiles[tile.x + 1, tile.y].GetComponent<AbstractTile>());
        }
        if (tile.y < height - 1)
        {
            neighbours.Add(tiles[tile.x, tile.y + 1].GetComponent<AbstractTile>());
        }
        return neighbours;
    }
}
