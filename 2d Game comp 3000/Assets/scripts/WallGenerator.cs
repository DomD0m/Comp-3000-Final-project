using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorpositions, tilemapvisual tilemapvisual)
    {
        var basicWallPosition = FindWallsInDirections(floorpositions, direction2D.cardinaldirectionslist);
        foreach(var position in basicWallPosition)
        {
            tilemapvisual.PaintSingleBasicWall(position);
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
