using UnityEngine;
using System.Collections;


public class TileLoader {

	public static TileMap loadTileMapFromFile(string fileName) {
		string[] lines = System.IO.File.ReadAllLines (fileName);
		TileMap tilemap = null;
		int height = lines.GetLength ();
		int width;
		int i = 0, j = 0;

		foreach (string line in lines){
			j = 0;
			string[] tileStrings = line.Split (',');
			width = tileStrings.GetLength ();
			tilemap = tilemap ?? new TileMap (width, height);
			foreach (string tileAndElevation in tileStrings) {
				string[] typeAndElevation = tileAndElevation.Split ('@');
				tilemap.setTile(TileFactory.makeTile(typeAndElevation[0], j++, i, tilemap, typeAndElevation[1])); 
			}
			i++;
		}

		return tilemap;
	}
}
