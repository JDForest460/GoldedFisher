using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_controller_squid : MonoBehaviour
{
    public Rigidbody2D rb_fish;
    public Rigidbody2D rb_boat;
    public Transform top_bound;
    public Transform bot_bound;
    public GameObject game_manage;
    public GameObject ink;
    public BoxCollider2D coll_fish;
    public int fish_value;
    private bool go_down = false;
    public float fish_speed;
    private float top_y;
    private float bot_y;
    private bool is_cauthed = false;
    private GameObject hook;
    private bool create_ink = false;
    // Start is called before the first frame update
    void Start()
    {
        top_y = top_bound.position.y;
        bot_y = bot_bound.position.y;
        Destroy(top_bound.gameObject);
        Destroy(bot_bound.gameObject);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!is_cauthed)
        {
  
            fish_move_topbot();
        }
        if (is_cauthed) pull_fish();
    }

    void fish_move_topbot()
    {

        if (go_down)
        {
            rb_fish.velocity = new Vector2(-fish_speed * Time.fixedDeltaTime, -fish_speed * Time.fixedDeltaTime);

            //Debug.Log(-fish_speed + rand_speed);
            if (rb_fish.transform.position.y <= bot_y)
            {
                rb_fish.transform.localScale = new Vector3(1, 1, 1);
                go_down = false;
            }
        }
        else
        {

            rb_fish.velocity = new Vector2(fish_speed * Time.fixedDeltaTime, fish_speed * Time.fixedDeltaTime);
            //Debug.Log(-fish_speed + rand_speed);
            if (rb_fish.transform.position.y >= top_y)
            {
                rb_fish.transform.localScale = new Vector3(-1, -1, 1);
                go_down = true;
            }
        }
    }
    void pull_fish()
    {
        if (rb_fish.transform.position.y >= rb_boat.transform.position.y)
        {
            Destroy(rb_fish.gameObject);
            game_manage.GetComponent<game_controller>().add_score(fish_value); ;

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
			if (!create_ink)
			{
                create_ink = true;
                Vector2 new_position = rb_fish.transform.position;
                GameObject new_fish = Instantiate(ink, new_position, Quaternion.identity);
            }
        }
    }



}
