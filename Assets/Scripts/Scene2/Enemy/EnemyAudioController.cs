using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    public AudioSource hurt;
    // Start is called before the first frame update
    public void PlayHurtAudio()
    {
            print("HURT!");
            hurt.Play();
    }
}
