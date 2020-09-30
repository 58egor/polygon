using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedMove = 1.5f;
    Transform player;
    Rigidbody body;
    public float sensitivity = 5f; // чувствительность мыши
    Vector3 movement;
    public float DashTime = 0.5f;
    float CurrentDashTime = 0;
    public float DashSpeed = 5;
    bool DashActive = false;
    public float smooth = 3;
    public bool useSmooth;
    // Start is called before the first frame update
    void Start()
    {
        player = transform;
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    // Update is called once per frame
    public void Moving()
    {
        body.MovePosition(body.position + movement * speedMove * Time.fixedDeltaTime);
    }
    void dir()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }
    void rotation()
    {
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y)) - body.position;
        lookPos.y = 0; // поворот в плоскости ХZ

        if (useSmooth)
        {
            Quaternion playerRotation = Quaternion.LookRotation(lookPos);
            player.rotation = Quaternion.Lerp(player.rotation, playerRotation, smooth * Time.fixedDeltaTime);
        }
        else
        {
            player.rotation = Quaternion.LookRotation(lookPos);
        }
    }
    void FixedUpdate()
    {
        Moving();

    }
    private void Update()
    {
        dir();
        rotation();
    }
}
