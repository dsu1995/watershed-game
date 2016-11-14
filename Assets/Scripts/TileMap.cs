using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour
{

    public const float SIDE_LENGTH = 0.1f;

    public uint width = 12, height = 12;
    public GameObject SourceTile;
    public GameObject SinkTile;
    public GameObject GrassTile;
    public GameObject CommercialTile;
    public GameObject ResidentialTile;
    public GameObject IndustrialTile;
    public GameObject SurfaceWater;
    public Text moneyText;
    public Text[] tileTexts;
    public GameObject[] tileTypes;

    GameObject[,] tiles;
    bool selecting = false;
    uint startX, startY;

    private void updateTileCosts()
    {
        for (int i = 0; i < tileTypes.Length; ++i)
        {
            uint cost = calculateCost(tileTypes[i]);
            tileTexts[i].text = tileTexts[i].text.Substring(0, tileTexts[i].text.IndexOf(" ($") < 0 ? tileTexts[i].text.Length : tileTexts[i].text.IndexOf(" ($")) + (cost > 0 ? " ($" + calculateCost(tileTypes[i]).ToString() + ")" : "");
        }
    }

    private uint calculateCost(GameObject type)
    {
        uint cost = 0;
        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                if (tiles[i, j].GetComponent<AbstractTile>().selected && tiles[i, j].GetComponent<AbstractTile>().GetType() != type.GetComponent<AbstractTile>().GetType())
                {
                    cost += type.GetComponent<AbstractTile>().cost;
                }
            }
        }
        return cost;
    }

    public PopulaceManager populace
    {
        private set; get;
    }

    public float money
    {
        private set; get;
    }
    
    public void Initialize(string filename)
    {
        List < List < float >> elevations = new List<List<float>>();

        string[] lines = System.IO.File.ReadAllLines(filename);


        foreach (string line in lines)
        {
            List<float> row = new List<float>();
            string[] fields = line.Split(',');

            foreach (string field in fields)
            {
                float elevation = float.Parse(field);
                row.Add(elevation);
            }
            elevations.Add(row);
        }


        this.width = (uint) elevations[0].Count + 2;
        this.height = (uint) elevations.Count + 2;
      
        tiles = new GameObject[width, height];
        populace = new PopulaceManager();


        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                float tileHeight = 0;
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                    //if (i > 2 && i < 5 && j == 0)
                
                    if (Random.Range(0, 2) == 0)
                    {
                        tiles[i, j] = Instantiate(SourceTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(SinkTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    }
                }
                else
                {
                    //float tileHeight = (i == 2 || i == 5) ? 700 : 700 + (i - 5) * 100;
                    //if (j > 2 && j < 7 && i > 2 && i < 5) { tileHeight = 0; }
                    //if (i == 4 && j == height - 2) { tileHeight = 600; }
                    //float tileHeight = Random.Range(0, 1000);
                    tileHeight = elevations[(int) i - 1][(int) j - 1] * 300;
                    tiles[i, j] = Instantiate(GrassTile, new Vector3(i, j, tileHeight / 200), Quaternion.identity) as GameObject;
                }
                tiles[i, j].GetComponent<AbstractTile>().Initialize(i, j, this, tileHeight, SurfaceWater);
            }
        }
    }

    void Start()
    {
        money = 10000f;
        Initialize("output_elevations.csv");
    }

    public void Initialize()
    {
        tiles = new GameObject[width, height];
        populace = new PopulaceManager();

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                float tileHeight = 0;
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                    if(i > 2 && i < 5 && j == 0)
                    //if (Random.Range(0, 2) == 0)
                    {
                        tiles[i, j] = Instantiate(SourceTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(SinkTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    }
                }
                else
                {
                    tileHeight = (i == 2 || i == 5) ? 700 : 700 + (i - 5) * 100;
                    if (j > 2 && j < 7 && i > 2 && i < 5) { tileHeight = 0; }
                    if (i == 4 && j == height - 2) { tileHeight = 600; }
                    if (Random.Range(0, 2) == 0)
                    {
                        tiles[i, j] = Instantiate(ResidentialTile, new Vector3(i, j, tileHeight / 200.0f), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(GrassTile, new Vector3(i, j, tileHeight / 200.0f), Quaternion.identity) as GameObject;
                    }
                }
                tiles[i, j].GetComponent<AbstractTile>().Initialize(i, j, this, tileHeight, SurfaceWater);
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
        this.updateTileCosts();
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
            this.updateTileCosts();
        }
    }

    public void endSelect()
    {
        selecting = false;
        this.updateTileCosts();
    }

    public void addIncome(float income)
    {
        money += income;
    }

    public void switchSelectedToType(GameObject type) {
        uint cost = calculateCost(type);
        if (cost <= money) {
            money -= cost;
            moneyText.text = "$" + Mathf.Floor(money);
            for (int i = 0; i < tileTypes.Length; ++i)
            {
                tileTexts[i].text = tileTexts[i].text.Substring(0, tileTexts[i].text.IndexOf(" ($") < 0 ? tileTexts[i].text.Length : tileTexts[i].text.IndexOf(" ($"));
            }
            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < height; j++)
                {
                    if (tiles[i, j].GetComponent<AbstractTile>().selected && tiles[i, j].GetComponent<AbstractTile>().GetType() != type.GetComponent<AbstractTile>().GetType())
                    {
                        switchTile(i, j, type);
                    }
                    tiles[i, j].GetComponent<AbstractTile>().selected = false;
                }
            }
        }
        this.updateTileCosts();
    }

    private void switchTile(uint x, uint y, GameObject type) {
        AbstractTile oldTile = tiles[x, y].GetComponent<AbstractTile>();
        GameObject newTile = Instantiate(type, new Vector3(x, y, oldTile.elevation / 200.0f), Quaternion.identity) as GameObject;
        AbstractTile absTile = newTile.GetComponent<AbstractTile>();
        absTile.Initialize(x, y, this, oldTile.elevation, SurfaceWater, oldTile.waterLevel);
        
        Destroy(tiles[x, y]);
        tiles[x, y] = newTile;
    }
}
