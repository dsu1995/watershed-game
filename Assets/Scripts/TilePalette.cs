using UnityEngine;
using System.Collections;

public class TilePalette : MonoBehaviour {

    private TileMap map;

    public void setSelectedTo(GameObject type) {
        if (map == null) {
            // Clone since we're instantiating the tilemap from the prefab
            map = GameObject.Find("TileMap(Clone)").GetComponent<TileMap>();
        }
        map.switchSelectedToType(type);
    }
}
