using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorGen : SimpleWalkMap
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    public float roompercent = 0.8f;


    protected override void RunGeneration()
    {
        corridorfirstgen();
    }

    private void corridorfirstgen()
    {
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridor = createCorridors(floorPosition, potentialRoomPosition);

        HashSet<Vector2Int> roomposition = createRooms(potentialRoomPosition);

        List<Vector2Int> deadEnds = FindallDeadEnds(floorPosition);

        createRoomsAtDeadEnd(deadEnds, roomposition);

        floorPosition.UnionWith(roomposition);

        for (int i = 0; i < corridor.Count; i++)
        {
            corridor[i] = IncreaseCorridorSizeByOne(corridor[i]);
            floorPosition.UnionWith(corridor[i]);
        }

        tilemapvisual.Paintfloortile(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tilemapvisual);

    }

    private List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newcorridor = new List<Vector2Int>();
        Vector2Int previewdirection = Vector2Int.zero;
        for (int i = 1; i <corridor.Count; i++)
        {
            Vector2Int directiofromcell = corridor[i] - corridor[i - 1];
            if(previewdirection != Vector2Int.zero && directiofromcell != previewdirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newcorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previewdirection = directiofromcell;
            }
            else
            {
                Vector2Int newCorridorTileOffSet = getdirection90from(directiofromcell);
                newcorridor.Add(corridor[i - 1]);
                newcorridor.Add(corridor[i - 1] + newCorridorTileOffSet);
            }
        }
        return newcorridor;
    }

    private Vector2Int getdirection90from(Vector2Int directiofromcell)
    {
        if (directiofromcell == Vector2Int.up)
            return Vector2Int.right;
        if (directiofromcell == Vector2Int.down)
            return Vector2Int.left;
        if (directiofromcell == Vector2Int.right)
            return Vector2Int.down;
        if (directiofromcell == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }

    private void createRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach(var position in deadEnds)
        {
            if(roomFloors.Contains(position) ==  false)
            {
                var roomfloor = runRandomwalk(randomWalkData, position);
                roomFloors.UnionWith(roomfloor);
            }
        }
    }

    private List<Vector2Int> FindallDeadEnds(HashSet<Vector2Int> floorPosition)
    {
        List<Vector2Int> deadends = new List<Vector2Int>();
        foreach (var position in floorPosition)
        {
            int neighbourscount = 0;
            foreach (var direction in direction2D.cardinaldirectionslist)
            {
                if (floorPosition.Contains(position + direction))
                    neighbourscount++;
                
                
            }
            if (neighbourscount == 1)
                deadends.Add(position);
        }
        return deadends;
    }

    private HashSet<Vector2Int> createRooms(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomtocreatecount = Mathf.RoundToInt(potentialRoomPosition.Count * roompercent);

        List<Vector2Int> roomtocreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomtocreatecount).ToList();

        foreach (var roomPosition in roomtocreate)
        {
            var roomfloor = runRandomwalk(randomWalkData, roomPosition);
            roomPositions.UnionWith(roomfloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> createCorridors(HashSet<Vector2Int> floorPosition, HashSet<Vector2Int> potentialRoomPosition)
    {
        var currentPosition = startposition;
        potentialRoomPosition.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenration.randomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPosition.Add(currentPosition);
            floorPosition.UnionWith(corridor);
        }
        return corridors;
    }
}
