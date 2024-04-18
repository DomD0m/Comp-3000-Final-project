using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenration 
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int start, int walklength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(start);
        var prevpostion = start;

        for (int i = 0; i < walklength; i++)
        {
            var newposition = prevpostion + direction2D.getrandomCdirection();
            path.Add(newposition);
            prevpostion = newposition;
        }
        return path;
    }

    public static List<Vector2Int> randomWalkCorridor(Vector2Int startposition, int corridorlength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = direction2D.getrandomCdirection();
        var currentposition = startposition;
        corridor.Add(currentposition);

        for (int i = 0; i < corridorlength; i++)
        {
            currentposition += direction;
            corridor.Add(currentposition);
        }
        return corridor;
    }
    
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spacetosplit, int minwidth, int minheight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomlist = new List<BoundsInt>();
        roomQueue.Enqueue(spacetosplit);
        while (roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if(room.size.y >= minheight && room.size.x >= minwidth)
            {
                if (room.size.y >= minheight * 2)
                {
                    SplitHorizontally (minheight, roomQueue, room);
                }
                else if (room.size.x >= minwidth * 2)
                {
                    splitVertically(minwidth,  roomQueue, room);
                }
                else if (room.size.x >= minwidth && room.size.y >= minheight)
                {
                    roomlist.Add(room);
                }
                else
                {
                    if (room.size.y >= minheight && room.size.x >= minwidth)
                    {

                        if (room.size.x >= minwidth * 2)
                        {
                            splitVertically(minwidth, roomQueue, room);
                        }
                        else if (room.size.y >= minheight * 2)
                        {
                            SplitHorizontally( minheight, roomQueue, room);
                        }
                        else if (room.size.x >= minwidth && room.size.y >= minheight)
                        {
                            roomlist.Add(room);
                        }
                    }
                }
                
            }
        }
        return roomlist;
    }

    private static void splitVertically(int minwidth, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
     }

    private static void SplitHorizontally( int minheight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
    new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }
}

public static class direction2D
{
    public static List<Vector2Int> cardinaldirectionslist = new List<Vector2Int>
    {
        new Vector2Int(0,1), //going up
        new Vector2Int(1,0), //going right
        new Vector2Int(0,-1), //going down
        new Vector2Int(-1,0),  //going left
    };

    public static List<Vector2Int> Diagonaldirectionslist = new List<Vector2Int>
    {
        new Vector2Int(1,1), //going up Right
        new Vector2Int(1,-1), //going right Down
        new Vector2Int(-1,-1), //going down Left
        new Vector2Int(-1,1),  //going left Up
    };

    public static List<Vector2Int> EightDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1), //going up
        new Vector2Int(1,1), //going up Right
        new Vector2Int(1,0), //going right
        new Vector2Int(1,-1), //going right Down
        new Vector2Int(0,-1), //going down
        new Vector2Int(-1,-1), //going down Left
        new Vector2Int(-1,0),  //going left
        new Vector2Int(-1,1),  //going left Up
    };

    public static Vector2Int getrandomCdirection()
    {
        return cardinaldirectionslist[Random.Range(0, cardinaldirectionslist.Count)];
    }
}
