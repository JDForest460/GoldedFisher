using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_ui_controller : MonoBehaviour
{
    public GameObject boat;
    public GameObject game_manage;
    public Button button_puse;
    public Button button_resue;
    public Button button_quit;
    public GameObject main_ui;
    public GameObject ui_button_puse;
    public GameObject ui_menu;
    public Transform news_poistion;
    public GameObject news_BlueFish;
    public GameObject news_RedFish;
    public GameObject news_PufferFish;
    public GameObject news_swordFish;
    private string news_name;
    // Start is called before the first frame update
    void Start()
    {
        //new_feature(1);
        ui_menu.SetActive(false);
        button_puse.onClick.AddListener(click_puse);
        button_resue.onClick.AddListener(click_resue);
        button_quit.onClick.AddListener(click_quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void click_puse()
    {
        //Debug.Log("puse");
        ui_button_puse.SetActive(false);
        ui_menu.SetActive(true);
        boat.GetComponent<boat_controller>().set_puse(true);
        Time.timeScale = 0;
    }
    private void click_resue()
    {
        //Debug.Log("resue");
        ui_button_puse.SetActive(true);
        ui_menu.SetActive(false);
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
    }
    private void click_quit()
    {
        //Debug.Log("quit");
        game_manage.GetComponent<game_controller>().sent_score();
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("End");
    }
    private void click_continue()
    {
        //Debug.Log("continue");
        ui_button_puse.SetActive(true);
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
        Destroy(main_ui.transform.Find(news_name).gameObject);
    }

    public void new_feature(int which)
	{
        ui_button_puse.SetActive(false);
        //Debug.Log("news!!!");
        Time.timeScale = 0;
        boat.GetComponent<boat_controller>().set_puse(true);
        if(which == 1)
		{
            news_name = "news_BlueFish(Clone)";
            GameObject new_feature = Instantiate(news_BlueFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if(which == 2)
		{
            news_name = "news_RedFish(Clone)";
            GameObject new_feature = Instantiate(news_RedFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if (which == 3)
        {
            news_name = "news_PufferFish(Clone)";
            GameObject new_feature = Instantiate(news_PufferFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if (which == 4)
        {
            news_name = "news_SwordFish(Clone)";
            GameObject new_feature = Instantiate(news_swordFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }





    }
}
