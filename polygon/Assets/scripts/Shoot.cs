using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody Bullet;
    public float speed;
    public float damage;
    public int targets;
    public bool ready = false;
    public bool FristStart = true;
    float time = 0;
    float distance;
    List<Collider> hits = new List<Collider>();
    Rigidbody CreateBullet;
    // Update is called once per frame
    void Simulation()
    {
       // Debug.DrawRay(transform.position, transform.forward * 20, Color.green);
        int i = 0;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward);
        RaycastHit copy;
        if (hit.Length == 0)
        {
            Debug.Log("Целей нету");
            return;
        }
            for (int j = 0; j < hit.Length - 1; j++)//сортируем полученные цели по дистанции по возрастанияю
            {
                for (int i2 = j + 1; i2 < hit.Length; i2++)
                {
                    if (hit[j].distance > hit[i2].distance)
                    {
                        copy = hit[j];
                        hit[j] = hit[i2];
                        hit[i2] = copy;
                    }

                }
            }
        if (hits.Count == 0)
        {
            distance = hit[i].distance;
        }
        else
        {
            int Count = 1;
            while(Count!=0){
                Count = 0;
                for(int j = 0; j < hits.Count; j++)
                {
                    if (hit[i].collider == hits[j])
                    {
                        Count++;
                        i++;
                        break;
                    }
                }
            }
            distance = hit[i].distance;
        }
        time += Time.deltaTime;
        if (hit[i].collider.gameObject.layer == 9)
        {
            Debug.Log(hit[i].collider.name);
            hits.Add(hit[i].collider);
        }
        if (distance < time * speed && time * speed < distance + (speed / 20))
        {

            if (hit[i].collider.gameObject.layer == 8)
            {
                hit[i].collider.GetComponent<Enemy>().Damage(damage);
                hits.Add(hit[i].collider);
            }
           
            if (hit[i].collider.gameObject.layer == 0 || hits.Count >= targets)
            {
                Debug.Log(hit[i].collider.name);
                Destroy(CreateBullet.gameObject);
                Destroy(transform.gameObject);
            }
        }
        if (time * speed > distance + (speed / 20))
        {
            hits.Add(hit[i].collider);
        }
        }
    void FixedUpdate()
    {
        if (ready)
        {
            if (FristStart)
            {
                CreateBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as Rigidbody;//создаем префаб пули на сцене
                CreateBullet.velocity = transform.forward * speed;//заставляем пулю лететь
                FristStart = false;
            }
            Simulation();
        }
    }
}
