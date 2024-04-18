using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<loot> lootList = new List<loot>();

    loot getDroppedItem()
    {
        int randomNumber = Random.Range(1,101);
        List<loot> possibleItems = new List<loot>();
        foreach (loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("no loot");
        return null;
    }

    public void instantiateLoot(Vector3 spawnposition)
    {
        loot droppedItem = getDroppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnposition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.LootSprite;
        }
            
     }
}
