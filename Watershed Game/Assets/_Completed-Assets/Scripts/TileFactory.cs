using System;

public class TileFactory
{
	public static AbstractTile makeTile(string type, int i, int j, TileMap map, float elevation = 0, float waterLevel = 0){
		if (type == "grass") {
			// Kill me now
			return new GrassTile (i, j, map, elevation, waterLevel);
		} else if (type == "source") {
			// Holy crap
			return new SourceTile (i, j, map);
		} else if (type == "sink") {
			// Jesus christ
			return new SinkTile(i, j, map);
		}
		// I've lost a lot of brain cells at this point
		return null;
	}
}

