using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boat_controller : MonoBehaviour
{

    public Rigidbody2D rb_bow;
    public Rigidbody2D rb_hook;
    public Transform hook_top;
    public LineRenderer fish_line;
    public Transform puse_top_left;
    public Transform puse_bot_right;
    public Sprite bow_0;
    public Sprite bow_1;
    public Sprite bow_2;
    public SpriteRenderer sprite_bow;
    public float bow_rotate_speed;
    public float bow_shot_speed;
    public float max_line_len;
    public float pull_bow_force;
    private float line_len;
    private bool go_right = true;
    private bool stop_rotate = false;
    private bool is_shot = false;
    private bool is_pull = false;
    private float top_left_x;
    private float top_left_y;
    private float bot_right_x;
    private float bot_right_y;
    private bool is_puse;
    private Vector3 position_before_shoot;
    // Start is called before the first frame update
    void Start()
    {
        line_len = 1;
        get_PuseButtonArea();
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_puse)
		{
            draw_line();
            //if (!stop_rotate) auto_rotate_bow();
            if (Input.GetMouseButton(0) && !mouse_isinbutton() && !is_shot && ! is_pull)
            {
                rotate_bow();
                pull_bow();
            }
            if (Input.GetMouseButtonUp(0) && line_len > 1)
            {
                shot_hook();
                sprite_bow.sprite = bow_0;
                //Debug.Log("mouse up");
            }
            moving_hook();

        }
 
    }
    void pull_bow()
	{
        if (line_len <= max_line_len)
        {
            line_len += pull_bow_force * Time.deltaTime;
            if(line_len <  max_line_len / 3){
                sprite_bow.sprite = bow_0;
            }
			else if(line_len < max_line_len * 2 / 3)
            {
                sprite_bow.sprite = bow_1;
			}
			else
			{
                sprite_bow.sprite = bow_2;
            }
        }
    }
    void get_PuseButtonArea()
	{
        top_left_x = puse_top_left.position.x;
        top_left_y = puse_top_left.position.y;
        bot_right_x = puse_bot_right.position.x;
        bot_right_y = puse_bot_right.position.y;
    }
    bool mouse_isinbutton()
	{
        Vector2 mouse_position = Input.mousePosition;
        get_PuseButtonArea();
        if(mouse_position.x > top_left_x && mouse_position.x < bot_right_x && mouse_position.y < top_left_y && mouse_position.y > bot_right_y)
		{
            return true;
		}
		else
		{
            return false;
		}
	}
    void rotate_bow()
	{
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosOnScreen = Input.mousePosition;
        mousePosOnScreen.z = screenPos.z;
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        Vector2 mouse_vector = new Vector3(rb_bow.transform.position.x - mousePosInWorld.x, rb_bow.transform.position.y - mousePosInWorld.y);

        rb_bow.transform.up = mouse_vector;

        //Debug.Log("mouse position is" + rb_bow.transform.position.x + ","+ rb_bow.transform.position.y);


    }
    void auto_rotate_bow()
	{
        float angel_now = Vector3.Angle(rb_bow.transform.up, Vector3.right);
        // Debug.Log(angel_now);


        if (go_right)
		{
            if (angel_now <= 170)
			{
                Vector3 angel_next = new Vector3(0f, 0f, bow_rotate_speed);
                rb_bow.transform.Rotate(angel_next * Time.deltaTime);
			}
			else
			{
                go_right = false;
			}
              
		}
		else
		{
            if (angel_now >= 10)
            {
                Vector3 angel_next = new Vector3(0f, 0f, -bow_rotate_speed);
                rb_bow.transform.Rotate(angel_next * Time.deltaTime);
            }
            else
            {
                go_right = true;
            }
        }
         
	}
    void draw_line()
	{
        fish_line.SetPosition(0, hook_top.position);
        fish_line.SetPosition(1, rb_hook.transform.position);
    }
    void shot_hook()
    {


        if (!is_shot && !is_pull && !is_puse && !mouse_isinbutton())
        {
            //Debug.Log("click left mouse");
            stop_rotate = true;
            is_shot = true;

		}
		else
		{
            // Debug.Log("cant shoot!!");
        }

    } 
    void moving_hook() 
    {
        if (is_shot)
        {
 
            if (Vector3.Distance(hook_top.position, rb_hook.position) <= line_len)
            {
                rb_hook.transform.position += -rb_hook.transform.up * bow_shot_speed * Time.deltaTime;
			}
			else
			{
                stop_shoot();
			}
            
		}
		if(is_pull)
		{
            
            //Debug.Log(rb_hook.transform.localPosition.y);
            if (Vector3.Distance(hook_top.position, rb_hook.position) >= 0.1 && rb_hook.transform.localPosition.y <=0)
            {
                
                rb_hook.transform.position += rb_hook.transform.up * bow_shot_speed * Time.deltaTime;
			}
			else
			{
                rb_hook.transform.localPosition = position_before_shoot;
                is_pull = false;
                stop_rotate = false;
                line_len = 1;
            }
        }
    }
    public bool get_pullstate()
	{
        return is_pull;
	}
    public void stop_shoot()
	{
        is_shot = false;
        is_pull = true;
	}
    public bool get_ispull()
	{
        return is_pull;
	}
    public void set_puse(bool i)
	{
        is_puse = i;
	}
}
