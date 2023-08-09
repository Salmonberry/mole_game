using System;
using Domin.Event;
using UnityEngine;

public class Gopher : MonoBehaviour
{
    public float existTime;
    public float timer;

    private void OnMouseDown()
    {
        //被点击对象必须挂载一个collider，才能被检测到
        print("onMouseDown");
        Destroy(gameObject);
        ScoreCalculationEvent.Trigger(1);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= existTime)
        {
            Destroy(gameObject);
        }
    }

    public void UnenabledTapped()
    {
        var collider = gameObject.GetComponent<BoxCollider2D>();

        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}