using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Rigidbody body;
    public float RangeY;
    public float HeighDel=15;
    public float WeighthDel=15;
    Transform cam;
    public Vector3 worldPosition;
    public float Smooth = 1;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec= body.position;
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        Debug.Log("Mouseworld" + worldPosition);
        Rect rect = new Rect(0,0, Screen.width/ WeighthDel, Screen.height/ HeighDel);
        rect.center = new Vector2(vec.x, vec.z);
        Vector2 pos = new Vector2(worldPosition.x, worldPosition.z);
        Debug.DrawLine(worldPosition, vec, Color.green);
        pos = Vector2.Max(pos, rect.min);
        pos = Vector2.Min(pos, rect.max);
        Debug.DrawLine(new Vector3(rect.max.x,vec.y, rect.min.y), new Vector3(rect.min.x, vec.y, rect.min.y), Color.green);
        Debug.DrawLine(new Vector3(rect.min.x, vec.y, rect.max.y), new Vector3(rect.min.x, vec.y, rect.min.y), Color.green);
        Debug.DrawLine(new Vector3(rect.max.x, vec.y, rect.max.y), new Vector3(rect.min.x, vec.y, rect.max.y), Color.green);
        Debug.DrawLine(new Vector3(rect.max.x, vec.y, rect.max.y), new Vector3(rect.max.x, vec.y, rect.min.y), Color.green);
        Debug.Log("Mouse2" + worldPosition);
        Debug.Log("Center"+rect.center);
        Debug.Log("Max" + rect.max);
        Debug.Log("Min" + rect.min);
        vec.x = (vec.x + pos.x) / 2;
        vec.z = (vec.z + pos.y) / 2;
        vec.y += RangeY;
        //vec = Camera.main.WorldToScreenPoint(vec);
        cam.position = Vector3.Lerp(cam.position,vec,Time.deltaTime*Smooth);

    }
}
