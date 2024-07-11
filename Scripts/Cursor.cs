using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Cursors;

    private float[] arrPosX = {-2.0f, -0.7f, 0.7f, 2.0f};
    private int produce_number = 0;
    private bool my_turn = false;
    GameObject[] clone;
    bool standard_set = false;
    private int now;
    private int count = 0;
    private int sum = 0;
    GameObject obj; 
    float timer = 0f;
    int[,] bin = {
        {0, 0, 0, 0}, {0, 0, 0, 1}, {0, 0, 1, 0}, {0, 0, 1, 1},
        {0, 1, 0, 0}, {0, 1, 0, 1}, {0, 1, 1, 0}, {0, 1, 1, 1},
        {1, 0, 0, 0}, {1, 0, 0, 1}, {1, 0, 1, 0}, {1, 0, 1, 1},
        {1, 1, 0, 0}, {1, 1, 0, 1}, {1, 1, 1, 0}, {1, 1, 1, 1},
    };

    void Start(){
        int array_size = 4;
        clone = new GameObject[array_size];
        obj = GameObject.Find("player");
    }

    void Update()
    {
        if(my_turn){
            if(!standard_set){
                SpawnCursor(arrPosX[produce_number], produce_number!=0?0:1);
                if(produce_number==0) standard_set = true;
            }
            else if(count==4 ){ //또는 게이지 시간이 다 소모되었으면 
                //게이지 시간이 다 떨어졌다면 sum에 -250 
                //if(timer == 0) 게이지 멈추기 
                timer+=Time.deltaTime;
                if(timer>0.8f) my_turn_end(sum);
            }
            else if(Input.GetKeyDown (KeyCode.LeftArrow)){
                Destroy(clone[produce_number]);
                SpawnCursor(arrPosX[produce_number], 2);
                count++;
                if(bin[now, count-1]==0) sum+=10; // 타이밍 점수 추가하기
                if(produce_number!=0){
                    Destroy(clone[produce_number]);
                    SpawnCursor(arrPosX[produce_number], 1);
                    if(produce_number!=0) produce_number--;
                    else produce_number = 3;
                }
            }
            else if(Input.GetKeyDown (KeyCode.RightArrow)){
                Destroy(clone[produce_number]);
                SpawnCursor(arrPosX[produce_number], 3);
                count++;
                if(bin[now, count-1]==1) sum+=10; // 타이밍 점수 추가하기
                if(produce_number!=0){
                    Destroy(clone[produce_number]);
                    SpawnCursor(arrPosX[produce_number], 1);
                    if(produce_number!=0) produce_number--;
                    else produce_number = 3;
                }
            }
        }
        else if(count==4){
            if(timer==0) StopCoroutine("FunctionWithDelay");
            timer+=Time.deltaTime;
            if(timer>0.8f) my_turn_end(0);
        }
    }

    public void other_turn(int i){
        timer = 0f;
        count = 0;
        now = i;
        produce_number = 0;
        //_wait값 변경
        StartCoroutine("FunctionWithDelay");
    }

    IEnumerator FunctionWithDelay(){
        for(count = 0; count < 4; count++){
            yield return new WaitForSeconds(0.5f);
            SpawnCursor(arrPosX[produce_number], bin[now, produce_number]+2);
        }   
    }

    //게이지 속도 구현 (https://wergia.tistory.com/41)
    //주변에 포인트 띄우기
    //포인트 점수 체계 자세히 구현하기

    void SpawnCursor(float posX, int index){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, 0);
        clone[produce_number] = Instantiate(Cursors[index], spawnPos, Quaternion.identity);
        if(++produce_number==4) produce_number = 0;
    }
    public void my_turn_start(int i){
        my_turn = true;
        produce_number = 0;
        now = i;
        count = 0;
        timer = 0f;
        sum = 0;
        //gauge 속도 넘기기
    }

    void my_turn_end(int sum){
        my_turn = false;
        standard_set = false;
        obj.GetComponent<Player>().score += sum;
        for(int i = 0; i < 4; i++)
            Destroy(clone[i]);
        obj.GetComponent<Player>().game_turn_end(now);
    }
}
