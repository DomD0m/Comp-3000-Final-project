using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkMap : AbstractDungeon
{

    [SerializeField]
    protected RandomWalkData randomWalkData;

   protected override void RunGeneration()
    {

        HashSet<Vector2Int> floorposition = runRandomwalk(randomWalkData, startposition);
        tilemapvisual.clear();
         tilemapvisual.Paintfloortile(floorposition);
        WallGenerator.CreateWalls(floorposition, tilemapvisual);
        
    }

    protected HashSet<Vector2Int> runRandomwalk(RandomWalkData Parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorpositions = new HashSet<Vector2Int>();
        for (int i = 0; i < Parameters.iterations; i++)
        {
            var path = ProceduralGenration.SimpleRandomWalk(currentPosition, Parameters.walkLength);
            floorpositions.UnionWith(path);
            if (Parameters.startRandomlyIterations)
                currentPosition = floorpositions.ElementAt(Random.Range(0, floorpositions.Count));
        }
        return floorpositions;
    }


}
