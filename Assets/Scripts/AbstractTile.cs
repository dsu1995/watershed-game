using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractTile : MonoBehaviour {

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

    public float newWaterLevel
    {
        get; protected set;
    }

    public float elevation
    {
        get; protected set;
    }

    TileMap map;

    public virtual void Initialize(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
    {
        this.x = x;
        this.y = y;
        this.map = map;
        this.waterLevel = waterLevel;
        this.newWaterLevel = 0;
    }

    public abstract string getType();
    public abstract float getPermeability();

    public void startTurn()
    {
        newWaterLevel = waterLevel;
    }

    public void changeWaterLevel(float delta)
    {
        waterLevel += delta;
    }

    public virtual void recieveWater(float amountRecieved)
    {
        waterLevel += amountRecieved;
    }

    protected virtual void sendWater(float amountSent)
    {
        waterLevel -= amountSent;
    }

    public virtual void flowWater()
    {
        List<AbstractTile> neighbours = map.getNeighbours(this); //optimize later
        neighbours.Sort(delegate (AbstractTile tile1, AbstractTile tile2) {
            float tile1Height = tile1.waterLevel + tile1.elevation;
            float tile2Height = tile2.waterLevel + tile2.elevation;
            if (tile1Height > tile2Height)
            {
                return 1;
            }
            else if(tile1Height < tile2Height)
            {
                return -1;
            }
            else
            {
                return 0;
            }
            });
        float amountWaterSent = 0;
        foreach(AbstractTile tile in neighbours)
        {
            if(waterLevel + elevation - amountWaterSent > tile.waterLevel + tile.elevation)
            {
                sendWater(0.1f); // Fix this number
                tile.recieveWater(0.1f);
                amountWaterSent += 0.1f;
            }
            else
            {
                break;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        flowWater();
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, color, Mathf.Max((100f - waterLevel) / 100f, 0));
    }

}
