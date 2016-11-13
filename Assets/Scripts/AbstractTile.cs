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

    public virtual float waterLevel
    {
        get; protected set;
    }

    public virtual float newWaterLevel
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
    private Color color;
    private float lastWaterLevel = 0;
    public float waterThresholdLevel
    {
        get; protected set;
    }

    private GameObject SurfaceWater;

    public virtual void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWaterPrefab, float waterLevel = 0, float waterThresholdLevel = 0f)
    {
        this.x = x;
        this.y = y;
        this.map = map;
        this.elevation = elevation;
        this.waterLevel = waterLevel;
        this.newWaterLevel = waterLevel;
        this.waterThresholdLevel = waterThresholdLevel;
        this.color = gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        this.evaporationRate = 2f;
        this.precipitationRate = 0f;
        gameObject.transform.localScale = new Vector3(1, 1, elevation / 100.0f);

        this.SurfaceWater = Instantiate(SurfaceWaterPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;

        //this.surfaceWater = Instantiate(, new Vector3(x, y, tileHeight / 100.0f), Quaternion.identity) as GameObject;

        //Debug.Log("tile(" + x + "," + y + ") ")
        updateSurfaceWater();
    }

    private void updateSurfaceWater()
    {
        if (displayedWaterLevel() > 0 && getType() != "source")
        {
            SurfaceWater.SetActive(true);
            SurfaceWater.transform.localPosition = new Vector3(x, y, elevation / 100f + displayedWaterLevel() / 200f);
            SurfaceWater.transform.localScale = new Vector3(1, 1, displayedWaterLevel() / 100.0f);
        }
        else
        {
            SurfaceWater.SetActive(false);
        }
        
    }


    public abstract string getType();
    public abstract float getPermeability();

    public void precipitate()
    {
        //rain for some frames, then stop raining for some frames
        if (precipitationCurrentFrame == precipitationFrame)
        {
            precipitationCurrentFrame = 0;
            precipitationFrame = (uint)UnityEngine.Random.Range(300, 450);
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
        newWaterLevel = Math.Max(newWaterLevel - evaporationRate + precipitationRate, 0);
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
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
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

    public float displayedWaterLevel()
    {
        return Mathf.Max(waterLevel - waterThresholdLevel, 0f);
    }

    public virtual float income()
    {
        return 0f;
    }

    void FixedUpdate()
    {
        precipitate();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        flowWater();
    }
    void LateUpdate() {
        newTurn();
        if (Mathf.Abs(lastWaterLevel - waterLevel) > flicker)
        {
            updateSurfaceWater();
            lastWaterLevel = waterLevel;
        }
        if (selected)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", this.color);
        }
        map.addIncome(income());
    }

    void OnDestroy()
    {
        Destroy(SurfaceWater);
    }
}
