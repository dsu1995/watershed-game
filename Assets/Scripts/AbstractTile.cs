﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class AbstractTile : MonoBehaviour {

    public float waterSpeed;
    public float fillThreshold;
    public float flicker;
    private uint precipitationFrame = 0;
    private uint precipitationCurrentFrame = 0;
    private bool precipitating = false;

    public float precipitationRate
    {
        get; protected set;
    }

    public float evaporationRate
    {
        get; protected set;
    }

    public uint x
    {
        get; private set;
    }

    public uint y
    {
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

    public WaterFlowStrat waterFlower
    {
        protected get; set;
    }

    public bool selected
    {
        get; set;
    }

    private TileMap map;
    private Color origColor, curColor;
    private float lastWaterLevel = 0;

    public virtual void Initialize(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
    {
        this.x = x;
        this.y = y;
        this.map = map;
        this.elevation = elevation;
        this.waterLevel = waterLevel;
        this.newWaterLevel = waterLevel;
        this.origColor = Color.Lerp(Color.white, gameObject.GetComponent<SpriteRenderer>().color, Mathf.Max((1000 - elevation) / 1000, 0));
        this.curColor = this.origColor;
        this.evaporationRate = 2f;
        this.precipitationRate = 0f;

        //Debug.Log("tile(" + x + "," + y + ") ")
    }

    public abstract string getType();
    public abstract float getPermeability();

    public void precipitate()
    {
        //rain for some frames, then stop raining for some frames
        if (precipitationCurrentFrame == precipitationFrame)
        {
            precipitationCurrentFrame = 0;
            precipitationFrame = (uint)UnityEngine.Random.Range(30, 100);
            if (precipitating)
            {
                this.precipitationRate = 0f;
                precipitating = false;
            }
            else
            {
                this.precipitationRate = UnityEngine.Random.Range(1, 8);
                precipitating = true;
            }
        }
        precipitationCurrentFrame++;
    }

    public void newTurn()
    {
        newWaterLevel = Math.Max(newWaterLevel - evaporationRate, 0);
        waterLevel = newWaterLevel;
        //if (x == 1 && y > 2 && y < 6)
        //{
        //    Debug.Log("tile " + y + " waterLevel: " + waterLevel + " newWaterLevel: " + newWaterLevel);
        //}
    }

    public virtual void recieveWater(float amountRecieved)
    {
        newWaterLevel += amountRecieved;
    }

    protected virtual void sendWater(float amountSent)
    {
        newWaterLevel -= amountSent;
    }

    public virtual void sendWaterTo(AbstractTile tile, float amount)
    {
        sendWater(amount); // Fix this number
        tile.recieveWater(amount);
    }

    void OnMouseDown()
    {
        map.startSelect(x, y);
    }

    void OnMouseEnter()
    {
        map.continueSelect(x, y);
    }

    void OnMouseUp()
    {
        map.endSelect();
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
                sendWaterTo(tile, waterSpeed);
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
		precipitate();
        newTurn();
        if (Mathf.Abs(lastWaterLevel - waterLevel) > flicker)
        {
            this.curColor = Color.Lerp(Color.blue, this.origColor, Mathf.Max((fillThreshold - waterLevel) / fillThreshold, 0));
            gameObject.GetComponent<SpriteRenderer>().color = this.curColor;
            lastWaterLevel = waterLevel;
        }
        if (selected)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, this.curColor, 0.5f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = this.curColor;
        }
    }
}
