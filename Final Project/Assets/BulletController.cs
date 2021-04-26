using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigBod;
    private SpriteRenderer sprite;
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5.0f);

        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 0;
        sprite.sortingLayerName = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        rigBod.velocity = rigBod.transform.right * 20f;
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(5);
        }
        Destroy(gameObject);
        Instantiate(Resources.Load("BulletImpact"), transform.position, transform.rotation);

    }
}


