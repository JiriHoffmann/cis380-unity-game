using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplodeController : MonoBehaviour
{
    private SpriteRenderer sprite;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioData = GetComponent<AudioSource>();
        playSound();
        sprite.sortingOrder = 0;
        sprite.sortingLayerName = "Player";
        Destroy(gameObject, 0.6f);
    }

    void playSound(){
        
        audioData.Play(0);
    }
}
