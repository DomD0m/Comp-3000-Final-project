using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGen : SimpleWalkMap
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    public float roompercent = 0.8f;
    [SerializeField]
    public RandomWalkData walkdata;

    protected override void RunGeneration()
    {
        corridorfirstgen();
    }

    private void corridorfirstgen()
    {
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();

        createCorridors(floorPosition);

        tilemapvisual.Paintfloortile(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tilemapvisual);

    }

    private void createCorridors(HashSet<Vector2Int> floorPosition)
    {
        var currentPosition = startposition;

        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenration.randomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            floorPosition.UnionWith(corridor);
        }
    }
}
