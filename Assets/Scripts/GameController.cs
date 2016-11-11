using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject map;

	// Use this for initialization
	void Start () {
        TileMap tileMap = (Instantiate(map) as GameObject).GetComponent<TileMap>();
        tileMap.Initialize(10, 10);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
