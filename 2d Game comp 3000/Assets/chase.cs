using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    public int health;
    public int maxhealth;
    private float dazedTime;
    public float startDazedTime;
    public HealthBarBehavior healthBar;
    public int dmg;
    public PlayerAttack pa;
    private float distance;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        healthBar.setHealth(health, maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 directon = player.transform.position - transform.position;
        directon.Normalize();
        float angle = Mathf.Atan2(directon.y, directon.x) * Mathf.Rad2Deg;

        if(distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward);
        }

       if(health <= 0)
        {
            death();
        }

       if(dazedTime <= 0)
        {
            speed = 1;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
    }

    public void takedmg (int dmg)
    {
        dazedTime = startDazedTime;
        health -= dmg;
        healthBar.setHealth(health, maxhealth);
        Debug.Log("dmg taken");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pa.TakeDamage(dmg);
        }
    }

    public void death()
    {
        GetComponent<LootBag>().instantiateLoot(transform.position);
        audioManager.playSFX(audioManager.death);
        Destroy(gameObject);
    }
}
