using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject gauge;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] Number;

    public int score = 0;
    GameObject gauge_clone;
    GameObject Number_clone;
    GameObject obj;
    void Start()
    {
        game_turn_start(0);
    }

    void game_turn_start(int i){
        SpawnNumber(i);
        if(i%3==2){
            gauge_start(i);
            GameObject.Find("Cursor").GetComponent<Cursor>().my_turn_start(i);
        }
        else{
            GameObject.Find("Cursor").GetComponent<Cursor>().other_turn(i);
        }
    }

    public void game_turn_end(int i){
        Debug.Log(score);
        Destroy(gauge_clone);
        Destroy(Number_clone);
        if(i!=15) 
            game_turn_start(i+1);
    }

    void gauge_start(int i) {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0);
        gauge_clone = Instantiate(gauge, spawnPos, Quaternion.identity);
    }

    void SpawnNumber(int index){
        Vector3 spawnPos = new Vector3(0, 2.65f, 0);
        Number_clone = Instantiate(Number[index], spawnPos, Quaternion.identity);
    }

}
