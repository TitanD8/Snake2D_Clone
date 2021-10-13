using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    //public GameManager gm;

    public List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    public Vector2 direction = Vector2.right;
    public Vector2 lastDirection;


    public int initalSize = 4;
    public float speed;
    public float speedMultiplier;

    private void Start()    {
        ResetState();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveSnake());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        //Only allow turning up or down while moving in the x axis
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                this.direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.direction = Vector2.down;
            }
        }

        // Only allow turning left or right while moving in the y-axis
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.direction = Vector2.left;
            }
        }
    }

    private IEnumerator MoveSnake()
    {
        while (this.enabled)
        {
            // Set each segment's position to be the same as the one it follows. We
            // must do this in reverse order so the position is set to the previous
            // position, otherwise they will all be stacked on top of each other.
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }

            // Move the snake in the direction it is facing
            // Round the values to ensure it aligns to the grid
            float x = Mathf.Round(this.transform.position.x) + this.direction.x;
            float y = Mathf.Round(this.transform.position.y) + this.direction.y;

            this.transform.position = new Vector2(x, y);

            yield return new WaitForSeconds(1.0f / (this.speed * this.speedMultiplier));
        }
    }

    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    public void ResetState()
    {
        GameManager.ClearScore();
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        //Starts at 1 to avoid destroying the snake's head.
        for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        // Clear the list but add back the snake head
        _segments.Clear();
        _segments.Add(this.transform);

        //Starts at -1 since the snake's head is already in the list
        for(int i = 0; i< this.initalSize - 1; i++)
        {
            Grow();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Food")
        {
            Debug.Log("SnakePos: " + this.transform.position);
            GameManager.UpdateScore();
            //gm.score = gm.score + 1;
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            ResetState();
        }
        else if(other.tag == "Teleport")
        {
            if(direction == Vector2.left)
            {
                float y = this.transform.position.y;
                this.transform.position = new Vector3(13f, y, 0.0f);
            }
            else if(direction == Vector2.right)
            {
                float y = this.transform.position.y;
                this.transform.position = new Vector3(-13f, y, 0.0f);
            }
            else if(direction == Vector2.up)
            {
                float x = this.transform.position.x;
                this.transform.position = new Vector3(x,-13, 0.0f);
            }
            else if (direction == Vector2.down)
            {
                float x = this.transform.position.x;
                this.transform.position = new Vector3(x, 13, 0.0f);
            }
        }
    }
}
