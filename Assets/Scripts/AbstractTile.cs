using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractTile : MonoBehaviour {

    public float waterSpeed;
    public float fillThreshold;
    public float flicker;

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

    private TileMap map;
    private Color color;
    private float lastWaterLevel = 0;

    public virtual void Initialize(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
    {
        this.x = x;
        this.y = y;
        this.map = map;
        this.waterLevel = waterLevel;
        this.newWaterLevel = waterLevel;
        this.color = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public abstract string getType();
    public abstract float getPermeability();

    public void newTurn()
    {
        //if (x == 1 && y > 2 && y < 6)
        //{
        //    Debug.Log("tile " + y + " waterLevel: " + waterLevel + " newWaterLevel: " + newWaterLevel);
        //}
        waterLevel = newWaterLevel;
    }

    public virtual void recieveWater(float amountRecieved)
    {
        newWaterLevel += amountRecieved;
    }

    protected virtual void sendWater(float amountSent)
    {
        newWaterLevel -= amountSent;
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
            if(waterLevel - amountWaterSent >= waterSpeed && waterLevel + elevation - amountWaterSent > tile.waterLevel + tile.elevation)
            {
                sendWater(waterSpeed); // Fix this number
                tile.recieveWater(waterSpeed);
                amountWaterSent += waterSpeed;
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
    }
    void LateUpdate() {
        newTurn();
        if (Mathf.Abs(lastWaterLevel - waterLevel) > flicker)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, this.color, Mathf.Max((fillThreshold - waterLevel) / fillThreshold, 0));
            lastWaterLevel = waterLevel;
        }
    }
}
