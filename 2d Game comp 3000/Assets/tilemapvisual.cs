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
    private TileBase floortile, wallTop, wallSideRight, WallSideLeft, WallBottom, wallfull, wallInnerCornerDownLeft, wallInnerCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpLeft, wallDiagonalCornerDownRight, wallDiagonalCornerUpRight;

    public void Paintfloortile(IEnumerable<Vector2Int> floorpositions)
    {
        paintTiles(floorpositions, floorTilemap, floortile);
    }

    internal void PaintSingleBasicWall(Vector2Int position, string Binarytype)
    {
        int typeasInt = Convert.ToInt32(Binarytype, 2);
        TileBase tile = null;
        if (WallBinary.wallTop.Contains(typeasInt))
        {
            tile = wallTop;
        }
        else if (WallBinary.wallSideRight.Contains(typeasInt))
        {
            tile = wallSideRight;
        }
        else if (WallBinary.wallSideLeft.Contains(typeasInt))
        {
            tile = WallSideLeft;
        }
        else if (WallBinary.wallBottm.Contains(typeasInt))
        {
            tile = WallBottom;
        }
        else if (WallBinary.wallFull.Contains(typeasInt))
        {
            tile = wallfull;
        }
        if (tile!= null)
            paintSingleTile(WallTileMap, tile, position);
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

    internal void paintSinglecornerWall(Vector2Int position, string neighbourBinary)
    {
        int typeasInt = Convert.ToInt32(neighbourBinary, 2);
        TileBase tile = null;

        if (WallBinary.wallInnerCornerDownLeft.Contains(typeasInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallBinary.wallInnerCornerDownRight.Contains(typeasInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallBinary.wallDiagonalCornerDownLeft.Contains(typeasInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallBinary.wallDiagonalCornerUpLeft.Contains(typeasInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallBinary.wallDiagonalCornerDownRight.Contains(typeasInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallBinary.wallDiagonalCornerUpRight.Contains(typeasInt))
        {
            tile = wallDiagonalCornerUpRight;
        }

        if (tile != null)
            paintSingleTile(WallTileMap, tile, position);
    }
}
