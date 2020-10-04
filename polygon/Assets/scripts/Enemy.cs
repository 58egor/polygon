using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Damage(float damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            Destroy(transform.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
