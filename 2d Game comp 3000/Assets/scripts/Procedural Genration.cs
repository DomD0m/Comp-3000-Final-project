using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Vector2Int getrandomCdirection()
    {
        return cardinaldirectionslist[Random.Range(0, cardinaldirectionslist.Count)];
    }
}
