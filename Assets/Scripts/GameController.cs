using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text moneyText;
    public Text populationText;
    public Text happinessText;
    public Text employementText;
    public TileMap map;

    private int updateCounter = 0;

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update () {
        if (updateCounter % 60 == 0)
        {
            moneyText.text = "$" + Mathf.Floor(map.money);
            populationText.text = map.populace.getPopulationTotal() + " p";
            happinessText.text = "H: " + map.populace.getPopulationOverallHappiness().ToString("P");
            employementText.text = "E: " + map.populace.getEmployementRate().ToString("P");
            updateCounter = 0;
        }
        updateCounter++;
    }
}
