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
    void Start()
    {
        SpawnObject = Instantiate(Enemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
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
                SpawnObject.transform.position = Vector3.MoveTowards(SpawnObject.transform.position, MovingsPoints[ID].transform.position, Time.deltaTime*speed);
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
            SpawnObject = Instantiate(Enemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        }
    }
}
