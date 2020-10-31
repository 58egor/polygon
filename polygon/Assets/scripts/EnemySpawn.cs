using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    GameObject[] SpawnEnemy;
    public GameObject Enemy;
    public float MaxTime;
    float timer;
    public int score;
    int MaxEnemy = 0;
    bool TargetsActive = false;
    bool ButtonActive = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==9)
        {
            ButtonActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            ButtonActive = false;
        }
    }
    void Start()
    {
        timer = MaxTime;
        SpawnEnemy = new GameObject[SpawnPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && ButtonActive)
        {
            TargetsActive = !TargetsActive;
        }
        Debug.Log(TargetsActive);
        if (TargetsActive)
        {
            timer -= Time.deltaTime;
            SpawnWork();
            //Debug.Log("timer:" + timer);
            check();
        }
        else
        {
            DeleteTargets();
        }
    }
    void SpawnWork()
    {
        timer -= Time.deltaTime;
        //Debug.Log("timer:" + timer);
        if (timer <= 0)
        {
            int i;
            Vector3 vec;
            do
            {
                i = Random.Range(0, SpawnPoints.Length);
            } while (SpawnEnemy[i] != null && MaxEnemy < SpawnEnemy.Length);
            vec = SpawnPoints[i].transform.position;
            vec.y += Enemy.transform.position.y / 2;
            if (SpawnEnemy[i] == null)
            {
                Debug.Log("Spawn " + SpawnEnemy.Length);
                SpawnEnemy[i] = Instantiate(Enemy, vec, SpawnPoints[i].transform.rotation);
                // MaxEnemy++;
            }
            else
            {
                Debug.Log("место занято");
            }
            timer = MaxTime;
        }
    }
    void DeleteTargets()
    {
        for (int i = 0; i < SpawnEnemy.Length; i++)
        {
            if (SpawnEnemy[i] != null)
            {
                Destroy(SpawnEnemy[i].gameObject);
            }
        }
    }
    void check()
    {
        MaxEnemy = 0;
        for (int i = 0; i < SpawnEnemy.Length; i++)
        {
            if (SpawnEnemy[i] != null)
            {
                MaxEnemy++;
            }
        }
        Debug.Log("Max:"+MaxEnemy);
    }
}
