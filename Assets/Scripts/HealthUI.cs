using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    GameObject player;
    int health;



    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.gameObject.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0f,2.5f,0f));
        health = player.GetComponent<Character>().health;
        this.gameObject.GetComponent<Slider>().value = health;
    }
}
