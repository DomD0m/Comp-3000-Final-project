using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int health;
    public int maxhealth;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackpos;
    public float attackrange;
    public LayerMask whatisenemy;
    public int dmg;
    public HealthBar hp;
    public GameManagerScript gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
               
                Collider2D[] enemiestodmg = Physics2D.OverlapCircleAll(attackpos.position, attackrange, whatisenemy);
                for (int i = 0; i < enemiestodmg.Length; i++)
                {
                    enemiestodmg[i].GetComponent<chase>().takedmg(dmg);
                }
               
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        hp.setHealth(health, maxhealth);

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackpos.position, attackrange);
        }

    }

    void Heal(int amount)
    {
        health += amount;
        if(health > maxhealth)
        {
            health = maxhealth;
        }
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0 && !isDead)
        {
            isDead = true;
            gameObject.SetActive(false);
            gameManager.gameOver();
            Debug.Log("gone");
        }
    }
}
