﻿using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject tileMap;
    public Text moneyText;
    public Text populationText;
    public Text happinessText;

    private TileMap map;
    private float money;

    private int updateCounter = 1;

    // Use this for initialization
    void Start () {
        map = (Instantiate(tileMap) as GameObject).GetComponent<TileMap>();
        map.Initialize(10, 10);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update () {
        updateCounter++;
        if (updateCounter % 60 == 0)
        {
            moneyText.text = "$" + Mathf.Floor(map.money);
            populationText.text = map.populace.getPopulationTotal() + " p";
            happinessText.text = map.populace.getPopulationOverallHappiness().ToString("P");
            updateCounter = 1;
        }
    }
}
