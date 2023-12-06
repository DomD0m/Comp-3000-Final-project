using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeon : MonoBehaviour
{
    [SerializeField]
    protected tilemapvisual tilemapvisual = null;
    [SerializeField]
    protected Vector2Int startposition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapvisual.clear();
        RunGeneration();
    }

    protected abstract void RunGeneration();

}
