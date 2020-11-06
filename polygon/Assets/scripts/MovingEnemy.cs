using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPoint;
    GameObject SpawnObject;
    public GameObject[] MovingsPoints;
    public GameObject Enemy;
    Rigidbody body;
    Vector3 lastPos = new Vector3(0,0,0);
    public float speed = 10;
    int ID = 1;
    public float timeout = 3f;
    float timer;
    void Start()
    {
        timer = timeout;
        Vector3 pos = SpawnPoint.transform.position;
        pos.y += Enemy.transform.localScale.y;
        SpawnObject = Instantiate(Enemy, pos, SpawnPoint.transform.rotation);
        body = SpawnObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SpawnObject != null)
        {
            if (SpawnObject.transform.position != lastPos)
            {
                lastPos = SpawnObject.transform.position;
                Vector3 pos = MovingsPoints[ID].transform.position;
                pos.y += Enemy.transform.localScale.y;
                SpawnObject.transform.position = Vector3.MoveTowards(SpawnObject.transform.position, pos, Time.deltaTime*speed);
            }
            else
            {
                if (ID + 1 >= MovingsPoints.Length)
                {
                    ID = 0;
                    Debug.Log("сброс");
                }
                else
                {
                    ID += 1;
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timeout < 0)
            {
                Vector3 pos = SpawnPoint.transform.position;
                pos.y += Enemy.transform.localScale.y;
                SpawnObject = Instantiate(Enemy, pos, SpawnPoint.transform.rotation);
                timer = timeout;
            }
        }
    }
}
