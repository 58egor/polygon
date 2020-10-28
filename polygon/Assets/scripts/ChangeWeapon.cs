using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject[] Guns;
    public int Id=0;
    // Start is called before the first frame update
    void Start()
    {
        Change();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Id -= 1;
            Change();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Id += 1;
            Change();
        }
    }
    public void Change()
    {
        if (Id > 1)
        {
            Id = 0;
        }
        if (Id < 0)
        {
            Id = 1;
        }
        if (Id == 0)
        {
            Null();
            Guns[0].SetActive(true);
        }
        if (Id == 1)
        {
            Null();
            Guns[1].SetActive(true);
        }
    }
    void Null()
    {
        Guns[0].SetActive(false);
        Guns[1].SetActive(false);
    }
}
