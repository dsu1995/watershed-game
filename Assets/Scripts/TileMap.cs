using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour
{

    public const float SIDE_LENGTH = 0.1f;

    public GameObject SourceTile;
    public GameObject SinkTile;
    public GameObject GrassTile;
	public GameObject CommercialTile;
	public GameObject ResidentialTile;
	public GameObject IndustrialTile;

    public GameObject SurfaceWater;


    GameObject[,] tiles;
    uint width, height;
    bool selecting = false;
    uint startX, startY;

    public PopulaceManager populace
    {
        private set; get;
    }

    public float money
    {
        private set; get;
    }

    public void Initialize(uint w, uint h)
    {
        this.width = w + 2;
        this.height = h + 2;
        tiles = new GameObject[width, height];
        populace = new PopulaceManager();

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                    if(i > 2 && i < 5 && j == 0)
                    //if (Random.Range(0, 2) == 0)
                    {
                        tiles[i, j] = Instantiate(SourceTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        tiles[i, j].GetComponent<SourceTile>().Initialize(i, j, this, SurfaceWater);
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(SinkTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        tiles[i, j].GetComponent<SinkTile>().Initialize(i, j, this, SurfaceWater);
                    }
                }
                else
                {
                    float tileHeight = (i == 2 || i == 5) ? 700 : 400;
                    if (j > 2 && j < 7 && i > 2 && i < 5) { tileHeight = 0; }
                    if (i == 4 && j == height - 2) { tileHeight = 600; }
                    //float tileHeight = Random.Range(0, 1000);
                    //tiles[i, j] = Instantiate(GrassTile, new Vector3(i, j, tileHeight / 200.0f), Quaternion.identity) as GameObject;
                    //tiles[i, j].GetComponent<GrassTile>().Initialize(i, j, this, tileHeight, SurfaceWater);
                    Population pop;
                    tiles[i, j] = Instantiate(ResidentialTile, new Vector3(i, j, tileHeight / 200.0f), Quaternion.identity) as GameObject;
                    tiles[i, j].GetComponent<ResidentialTile>().Initialize(out pop, i, j, this, tileHeight, SurfaceWater);
                    populace.add(pop);
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

    public void startSelect(uint x, uint y)
    {
        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                bool selected = i == x && j == y && !tiles[i, j].GetComponent<AbstractTile>().selected;
                tiles[i, j].GetComponent<AbstractTile>().selected = selected;
            }
        }
        startX = x;
        startY = y;
        selecting = true;
    }

    public void continueSelect(uint x, uint y)
    {
        if (selecting)
        {
            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < height; j++)
                {
                    bool selected = ((i >= x && i <= startX) || (i >= startX && i <= x)) && ((j >= y && j <= startY) || (j >= startY && j <= y));
                    tiles[i, j].GetComponent<AbstractTile>().selected = selected;
                }
            }
        }
    }

    public void endSelect()
    {
        selecting = false;
    }

    public void addIncome(float income)
    {
        money += income;
    }

    void update () {
    }

    public void switchSelectedToType(GameObject type) {
        for (uint i = 0; i < width; i++) {
            for (uint j=0; j < height; j++) {
                if (tiles[i, j].GetComponent<AbstractTile>().selected) {
                    switchTile(i, j, type);
                    
                    //Debug.Log(i + " " + j);
                    //Debug.Log(tiles[i, j]);
                }
            }
        }
    }

    private void switchTile(uint x, uint y, GameObject type) {
        AbstractTile oldTile = tiles[x, y].GetComponent<AbstractTile>();
        GameObject newTile = Instantiate(type, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        AbstractTile absTile = newTile.GetComponent("AbstractTile") as AbstractTile;
        absTile.Initialize(x, y, this, oldTile.elevation, SurfaceWater, oldTile.waterLevel, oldTile.waterThresholdLevel);
        
        tiles[x, y] = newTile;
        Destroy(oldTile);

    }
}
