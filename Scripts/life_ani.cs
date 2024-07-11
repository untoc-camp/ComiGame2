using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life_ani : MonoBehaviour
{
    public float animSpeed = 2.0f;
    public Animator animator;
   
    // Update is called once per frame
    void Update ()
    {
        animator.SetFloat("gauge_speed", animSpeed);
    }

    void Start(){
        
    }

    public void GameOver()
    {
        GameObject.Find("player").GetComponent<Player>().game_turn_end(15);
    }
}
