using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int dmg;
    public LayerMask whatisSolid;

    private void Start()
    {
        Invoke("destroyProjectile", lifetime);
    }

    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance, whatisSolid);
        if (hitinfo.collider != null)
        {
            if (hitinfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("enemy hit");
                hitinfo.collider.GetComponent<chase>().takedmg(dmg);
            }
            destroyProjectile();
        }


        transform.Translate(transform.up * speed * Time.deltaTime);
    }

    void destroyProjectile()
    {
        Destroy(gameObject);
    }
}
