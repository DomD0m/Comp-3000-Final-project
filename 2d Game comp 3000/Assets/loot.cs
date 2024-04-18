using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class loot : ScriptableObject
{
    public Sprite LootSprite;
    public string LootName;
    public int dropChance;

    public loot (string lootName, int dropChance)
    {
        this.LootName = lootName;
        this.dropChance = dropChance;
    }
}
