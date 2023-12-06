using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemapvisual : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, WallTileMap;
    [SerializeField]
    private TileBase floortile, wallTop;

    public void Paintfloortile(IEnumerable<Vector2Int> floorpositions)
    {
        paintTiles(floorpositions, floorTilemap, floortile);
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        paintSingleTile(WallTileMap, wallTop, position);
    }
    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions)
        {
            paintSingleTile(tilemap, tile, position);
        }
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void clear()
    {
        floorTilemap.ClearAllTiles();
        WallTileMap.ClearAllTiles();
    }
}
