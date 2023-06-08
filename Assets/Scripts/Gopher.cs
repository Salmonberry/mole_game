using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Gopher : MonoBehaviour
{
    public float existTime;
    public float timer;

    private void OnMouseDown()
    {
        //被点击对象必须挂载一个collider，才能被检测到
        print("onMouseDown");
        Destroy(gameObject);
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= existTime)
        {
            Destroy(gameObject);
        }
    }
}