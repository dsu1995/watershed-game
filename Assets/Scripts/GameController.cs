using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject tileMap;
    public Text moneyText;

    private TileMap map;
    private float money;

    // Use this for initialization
    void Start () {
        map = (Instantiate(tileMap) as GameObject).GetComponent<TileMap>();
        map.Initialize(10, 10);
    }

    // Update is called once per frame
    void Update () {
        moneyText.text = "$" + Mathf.Floor(map.money);
    }
}
