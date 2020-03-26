using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCount : MonoBehaviour
{
    private GameManager gameManager = null;
    public Text time;
    
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager)
        {
            gameManager = GameManager.FindObjectOfType<GameManager>();
        }
        else
        {
            time.text = gameManager.time.ToString("#.#");
        }
    }
}
