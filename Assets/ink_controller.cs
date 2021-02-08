using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ink_controller : MonoBehaviour
{
    public GameObject ink;
    public float time_left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time_left -= Time.deltaTime;
        if (time_left <= 0) Destroy(ink);
    }
}
