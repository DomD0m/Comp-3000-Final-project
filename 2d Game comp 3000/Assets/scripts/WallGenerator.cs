using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorpositions, tilemapvisual tilemapvisual)
    {
        var basicWallPosition = FindWallsInDirections(floorpositions, direction2D.cardinaldirectionslist);
        var cornerWallPosition = FindWallsInDirections(floorpositions, direction2D.Diagonaldirectionslist);
        createbasicwalls(tilemapvisual, basicWallPosition, floorpositions);
        createCornerwalls(tilemapvisual, cornerWallPosition, floorpositions);
    }

    private static void createCornerwalls(tilemapvisual tilemapvisual, HashSet<Vector2Int> cornerwallposition, HashSet<Vector2Int> floorpositions)
    {
        foreach (var position in cornerwallposition)
        {
            string NeighbourBinary = "";
            foreach (var direction in direction2D.EightDirections)
            {
                var neighbourposition = position + direction;
                if (floorpositions.Contains(neighbourposition))
                {
                    NeighbourBinary += "1";
                }
                else
                {
                    NeighbourBinary += "0";
                }
            }
            tilemapvisual.paintSinglecornerWall(position, NeighbourBinary);
        }
    }

    private static void createbasicwalls(tilemapvisual tilemapvisual, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorpositions)
    {
        foreach (var position in basicWallPosition)
        {
            string NeighboursBinary = "";
            foreach ( var direction in direction2D.cardinaldirectionslist)
            {
                var neighbourposition = position + direction;
                if (floorpositions.Contains(neighbourposition))
                {
                    NeighboursBinary += "1";
                }else
                {
                    NeighboursBinary += "0";
                }
            }
            tilemapvisual.PaintSingleBasicWall(position, NeighboursBinary);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorpositions, List<Vector2Int> directionlist)
    {
        HashSet<Vector2Int> wallposition = new HashSet<Vector2Int>();
        foreach (var position in floorpositions)
        {
            foreach (var direction in directionlist)
            {
                var neighbourPosition = position + direction;
                if (floorpositions.Contains(neighbourPosition) == false)
                    wallposition.Add(neighbourPosition);
            }
        }
        return wallposition;     
    }
}
