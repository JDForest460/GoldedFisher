using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_controller : MonoBehaviour
{
    public Rigidbody2D rb_fish;
    public Transform left_bound;
    public Transform right_bound;
    private bool face_left = true;
    public float fish_speed;
    private float left_x;
    private float right_x;
    // Start is called before the first frame update
    void Start()
    {
        left_x = left_bound.position.x;
        right_x = right_bound.position.x;
        Destroy(left_bound.gameObject);
        Destroy(right_bound.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        fish_move();
    }
    void fish_move()
	{
        float rand_speed = Random.Range(0f, 500f);

        if (face_left)
		{
            rb_fish.velocity = new Vector2( (-fish_speed - rand_speed) * Time.deltaTime, rb_fish.velocity.y);

            //Debug.Log(-fish_speed + rand_speed);
            if(rb_fish.transform.position.x < left_x)
			{
                rb_fish.transform.localScale = new Vector3(-1, 1, 1);
                face_left = false;
			}
		}
		else
		{

            rb_fish.velocity = new Vector2( (fish_speed + rand_speed) * Time.deltaTime, rb_fish.velocity.y);
            //Debug.Log(-fish_speed + rand_speed);
            if (rb_fish.transform.position.x > right_x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                face_left = true;
            }
        }
	}
}
