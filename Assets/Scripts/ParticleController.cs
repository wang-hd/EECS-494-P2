using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem spark;
    Subscription<GameEndEvent> game_end_sub;
    // Start is called before the first frame update
    void Start()
    {
        spark = GetComponent<ParticleSystem>();
        game_end_sub = EventBus.Subscribe<GameEndEvent>(SparkAtWin);
    }

    void SparkAtWin(GameEndEvent e) 
    {
        if (e.win) spark.Play();

    }
}
