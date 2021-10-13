using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public SnakeController snake;
    public Collider2D gridArea;

    private void Start()
    {
        RandomisePosition();
    }

    private void RandomisePosition()
    {
        Bounds bounds = gridArea.bounds;
        bool retry = false;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        // this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        Vector3 nextPos = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        foreach(Transform part in snake._segments)
        {
            if (nextPos == part.transform.position)
            {
                retry = true;
            }
            else
            {
                Debug.Log("Occupied");
                //Do Nothing
            }
        }
        if(retry != true)
        {
            this.transform.position = nextPos;
        }
        else
        {
            RandomisePosition();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
           
           
            RandomisePosition();
        }
    }
}
