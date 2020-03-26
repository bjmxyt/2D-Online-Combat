using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthBarControl : MonoBehaviour
{
    public Slider healthBar;
    public CombatSystem player;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = player.Health / 100;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(healthBar.value <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
