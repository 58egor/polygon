using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("Основные параметры")]
    public GameObject gunpoit;
    public int speed;
    public int damage;
    public int targets;
    public float Timeout;
    private float curTimeout;
    [Header("Параметр для луча")]
    public float Distance;
    [Header("Создаваемы объекты при стрельбе объектом")]
    public Transform GunRay;
    public Rigidbody Bullet;
    [Header("Параметры для разборса при самом выстреле")]
    public bool ActiveRazbros;
    public float razbros;
    public int bullets;
    [Header("Параметры для разброса при долгом зажатии")]
    [Header("Тип стрельбы")]
    public bool Ray;
    public bool RayBullet;
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
                if (RayBullet)
                {
                    RayBul();
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
    //симуляция стрельбы объектом с лучом
    void RayBul()
    {
        CreateBullet(transform.parent.rotation);
        if (ActiveRazbros)
        {
            for(int i = 0; i < bullets;i++)
            {
                Vector3 rot = transform.parent.rotation.eulerAngles;
                rot.y += razbros * (i + 1);
                CreateBullet(Quaternion.Euler(rot));
            }
            for (int i = 0; i < bullets; i++)
            {
                Vector3 rot = transform.parent.rotation.eulerAngles;
                rot.y -= razbros * (i + 1);
                CreateBullet(Quaternion.Euler(rot));
            }
        }
    }
    void CreateBullet(Quaternion rot)
    {
        Transform obj;
        obj = Instantiate(GunRay, gunpoit.transform.position, rot);
        obj.GetComponent<Shoot>().damage = damage;
        obj.GetComponent<Shoot>().speed = speed;
        obj.GetComponent<Shoot>().targets = targets;
        obj.GetComponent<Shoot>().Bullet = Bullet;
        obj.GetComponent<Shoot>().ready = true;
    }
}
