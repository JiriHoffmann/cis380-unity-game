using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigBod;
    GameObject Rocket;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        Rocket = GameObject.Find("Rocket");
        rigBod = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10.0f);

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
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(20, player.transform.position.x < transform.position.x ? 1 : -1);
        }
        Destroy(gameObject);
        Instantiate(Resources.Load("EnemyExplode"), transform.position, transform.rotation);

    }
}
