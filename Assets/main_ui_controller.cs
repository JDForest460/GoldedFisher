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
    public GameObject ui_button_puse;
    public GameObject ui_menu;
    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("quit");
        game_manage.GetComponent<game_controller>().sent_score();
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("End");
    }
}
