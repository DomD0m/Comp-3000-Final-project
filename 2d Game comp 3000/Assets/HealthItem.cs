using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healamount;
    public GameObject player;
    public PlayerAttack play;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int current = play.health += healamount;
            if (current > play.maxhealth)
            {
                current = play.maxhealth;
            }
            play.health = current;

            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }


}
