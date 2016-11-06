using UnityEngine;
using System.Collections;




public class TileMap {

    public const float SIDE_LENGTH = 0.1f;

    AbstractTile[,] tiles;

    uint width, height;

	public TileMap(uint width, uint height)
    {
        this.width = width;
        this.height = height;
        tiles = new AbstractTile[width, height];

        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                tiles[i,j] = new GrassTile(i, j, this, Random.Range(0, 1000));
            }
        }
    }

    public void draw(Mesh mesh)
    {
        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        Color32[] colors = new Color32[vertices.Length];

        uint index = 0;
        for (uint i = 0; i < width + 1; i++)
        {
            for (uint j = 0; j < height + 1; j++)
            {
                colors[index] = getColor();
                vertices[index++] = new Vector3(i * SIDE_LENGTH, j * SIDE_LENGTH, 0);
            }
        }

        int[] triangles = new int[3 * 2 * width * height];
        

        int triangleIndex = 0;
        int topLeftIndex = 0;
        int topRightIndex = topLeftIndex + 1;
        int bottomLeftIndex = (int) width + 1;
        int bottomRightIndex = bottomLeftIndex + 1;
        for (uint i = 0; i < width; i++)
        {
            for (uint j = 0; j < height; j++)
            {
                //Color32 color = getColor(tiles[i, j]);
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = bottomRightIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = topRightIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = topLeftIndex;

                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = topLeftIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = bottomLeftIndex;
                //colors[triangleIndex] = color;
                triangles[triangleIndex++] = bottomRightIndex;

                topLeftIndex++;
                topRightIndex++;
                bottomLeftIndex++;
                bottomRightIndex++;
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors32 = colors;
    }

    private Color32 getColor()
    {
        return Random.ColorHSV();
    }
}
