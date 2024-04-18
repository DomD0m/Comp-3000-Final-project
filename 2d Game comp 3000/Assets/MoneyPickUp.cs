using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPickUp : MonoBehaviour
{
    AudioManager audioManager;
    public int cashValue = 1;
    public TMP_Text counterText;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IInventory inventory = collision.GetComponent<IInventory>();

            if(inventory != null)
            {
                inventory.Money = inventory.Money + cashValue;
                print("player inventory has " + inventory.Money + "money in it");
                counterText.text = "Money: " + inventory.Money;
                audioManager.playSFX(audioManager.Gem);
                gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

}
