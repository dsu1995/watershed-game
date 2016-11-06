using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TileMap map = new TileMap(10, 10);
        Mesh mesh = new Mesh();
        map.draw(mesh);
        GetComponent<MeshFilter>().sharedMesh = mesh;
        MeshFilter mf = GetComponent<MeshFilter>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
