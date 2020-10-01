using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject gunpoit;
    public int speed;
    public int damage;
    public int targets;
    public float Timeout;
    private float curTimeout;
    public float Distance;
    [Header("Тип стрельбы")]
    public bool Ray;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(gunpoit.transform.position, gunpoit.transform.forward * Distance, Color.green);
        if (Input.GetMouseButton(0))
        {
            curTimeout += Time.deltaTime;
            if (curTimeout > Timeout)
            {
                curTimeout = 0;
                if (Ray)
                {
                    RayShoot();
                }

            }
        }
        else
        {
            curTimeout = Timeout + 1;
        }
    }
    //функция отвечающая за стрельбу лучом
    void RayShoot()
    {
        int targ = 0;
        RaycastHit[] hit;
        Ray ray = new Ray(gunpoit.transform.position, gunpoit.transform.forward);
        hit = Physics.RaycastAll(ray,Distance);
        RaycastHit copy;
        if (hit.Length != 0)
        {
            for (int i = 0; i < hit.Length - 1; i++)//сортируем полученные цели по дистанции по возрастанияю
            {
                for (int i2 = i + 1; i2 < hit.Length; i2++)
                {
                    if (hit[i].distance > hit[i2].distance)
                    {
                        copy = hit[i];
                        hit[i] = hit[i2];
                        hit[i2] = copy;
                    }

                }
            }
            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject.layer == 8)
                {
                    hit[i].transform.GetComponent<Enemy>().Damage(damage);
                    targ++;
                }
                if (targ == targets)
                {
                    break;
                }
            }
        }
    }
}
