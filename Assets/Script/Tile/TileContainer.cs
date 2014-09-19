using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

partial class TileContainer : MonoBehaviour
{
    private static TileContainer instance = null;

    public static TileContainer Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(TileContainer)) as TileContainer;
            return instance;
        }
    }
}

partial class TileContainer : MonoBehaviour
{
    private Tile[,] _tiles;
    public Tile getTile(int y, int x)
    {
        //1부터 시작이 아닌 0부터 시작이므로
        return _tiles[y - 1, x - 1];
    }
}
partial class TileContainer : MonoBehaviour
{
    void Start()
    {
        _tiles = new Tile[Const.TileHeight, Const.TileHeight];
        _findTiles();
    }
}

partial class TileContainer : MonoBehaviour
{
    private void _findTiles()
    {
        Tile[] tile_instances = (Tile[])FindObjectsOfType(typeof(Tile));
        int width, height;
        foreach (Tile tile_instance in tile_instances)
        {
            _getCoordinate(tile_instance.name, out height, out width);
            //Debug.Log("height : " + height + " , width : " + width);
            //1부터 시작이 아닌 0부터 시작이므로
            _tiles[height - 1, width - 1] = tile_instance;
        }
    }

    private void _getCoordinate(string tileName, out int height, out int width)
    {
        //"Tile1-1" => out 1(height) , out 1(width)
        string remove_tile_str = Regex.Replace(tileName, @"Tile", "");
        string[] splitted_str = Regex.Split(remove_tile_str, "-");
        width = int.Parse(splitted_str[0]);
        height = int.Parse(splitted_str[1]);
    }
}