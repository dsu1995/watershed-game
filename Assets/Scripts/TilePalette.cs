using UnityEngine;
using System.Collections;

public class TilePalette : MonoBehaviour {

    public TileMap map;

    public void setSelectedTo(GameObject type) {
        map.switchSelectedToType(type);
    }
}
