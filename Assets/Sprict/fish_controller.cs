﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_controller : MonoBehaviour
{
    public Rigidbody2D rb_fish;
    public Rigidbody2D rb_boat;
    public Transform left_bound;
    public Transform right_bound;
    public BoxCollider2D coll_fish;
    private bool face_left = true;
    public float fish_speed;
    private float left_x;
    private float right_x;
    private bool is_cauthed = false;
    private GameObject hook;
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
        if (!is_cauthed) fish_move();
        if (is_cauthed) pull_fish();
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
    void pull_fish()
	{   
        if(rb_fish.transform.position.y >= rb_boat.transform.position.y)
		{
            Destroy(rb_fish.gameObject);
            rb_boat.gameObject.GetComponent<boat_controller>().add_score(1);

        }
        rb_fish.transform.position = hook.GetComponent<Rigidbody2D>().transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hook" && !rb_boat.gameObject.GetComponent<boat_controller>().get_pullstate())
        {
            
            // collision.gameObject.transform.position
            is_cauthed = true;
            hook = collision.gameObject;
            rb_fish.velocity = new Vector2(0, 0);
            
        }
    }

}
