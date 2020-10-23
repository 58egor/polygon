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
    public int MaxOboima;
    public int Oboima;
    public float TimeOfReload;
    private float ReloadTime;
    public int AddPatron;
    public bool Reload = false;
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
    public bool BulletRazbros;
    float degre=0;
    public float MaxDegre;
    public float AddDegre = 1;
    public int NumberOfShoots;
    int ShootActive;
    int ShootNotActive;
    private float ShootTimeout;
    [Header("N-выстрел")]
    private bool TripleShoot=false;
    public bool TripleShootActive;
    public int Nshoots;
    private int Shoots = 0;
    public float TimeBetweenShoots;
    private float TimeBetween=0;
    [Header("Тип стрельбы")]
    public bool Ray;
    public bool RayBullet;
    // Start is called before the first frame update
    void Start()
    {
        ShootActive = ShootNotActive = NumberOfShoots;
        Oboima = MaxOboima;
        ReloadTime = TimeOfReload;
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if(Oboima<MaxOboima && !Reload)
            {
                Reload = true;
            }
        }
    }
        // Update is called once per frame
        void Update()
    {
        Debug.DrawRay(gunpoit.transform.position, gunpoit.transform.forward * Distance, Color.green);
        if ((Input.GetMouseButton(0) || TripleShoot))
        {
            if (TripleShootActive && TripleShoot)
            {
                TimeBetween += Time.deltaTime;
                curTimeout = 0;
            }
            if (!TripleShoot)
            {
                curTimeout += Time.deltaTime;
            }
            if (Oboima > 0)
            {
                Reload = false;
            }
            if ((curTimeout > Timeout || TimeBetween> TimeBetweenShoots) && !Reload)
            {
                Oboima--;
                curTimeout = 0;
                TimeBetween = 0;
                if (TripleShootActive)
                {
                    if (TripleShoot)
                    {
                        if (Shoots < Nshoots-1)
                        {
                            Shoots++;
                        }
                        else
                        {
                            TripleShoot = false;
                            TimeBetween = 0;
                            Shoots=0;

                        }
                    }
                    else
                    {
                        TripleShoot = true;
                        Shoots++;
                    }
                }
                if (Ray)
                {
                    RayShoot();
                }
                if (RayBullet)
                {
                    RayBul();
                }
                if (BulletRazbros)
                {
                    ShootActive--;
                    ShootNotActive = NumberOfShoots;
                    if (ShootActive == 0)
                    {
                        if (degre < MaxDegre)
                        {
                            degre += AddDegre;
                        }
                        else
                        {
                            degre = MaxDegre;
                        }
                        ShootActive = NumberOfShoots;
                    }
                }
            }
        }
        else
        {
            if(!TripleShoot)
            curTimeout += Time.deltaTime;
            if (BulletRazbros)
            {
                ShootTimeout += Time.deltaTime;
                if (ShootTimeout > Timeout)
                {
                    ShootNotActive--;
                    ShootActive = NumberOfShoots;
                    if (ShootNotActive == 0)
                    {
                        if (degre > 0)
                        {
                            degre -= AddDegre;
                        }
                        else
                        {
                            degre = 0;
                        }
                        ShootNotActive = NumberOfShoots;
                    }
                }
            }
        }
        if (Oboima == 0 || Reload)
        {
            Reload = true;
            Rel();  
        }
    }
    void Rel()
    {
        ReloadTime -= Time.deltaTime;
        if (ReloadTime < 0)
        {
            Oboima += AddPatron;
            ReloadTime = TimeOfReload;
            if (Oboima >= MaxOboima)
            {
                if (Oboima > MaxOboima)
                {
                    
                    Oboima -= (Oboima - MaxOboima);
                }
                Reload = false;
                
            }
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
        Vector3 rot = transform.parent.rotation.eulerAngles;
        rot.y += Random.Range(-degre, degre);
        CreateBullet(Quaternion.Euler(rot));
        if (ActiveRazbros)
        {
            for(int i = 0; i < bullets;i++)
            {
                rot = transform.parent.rotation.eulerAngles;
                rot.y += razbros * (i + 1)+ Random.Range(-degre, degre);
                CreateBullet(Quaternion.Euler(rot));
            }
            for (int i = 0; i < bullets; i++)
            {
                rot = transform.parent.rotation.eulerAngles;
                rot.y -= razbros * (i + 1)+ Random.Range(-degre, degre);
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
