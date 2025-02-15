using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioScript : MonoBehaviour
{
    [SerializeField] float volume_value=1.5f;
    public AudioSource audio_button;
    public AudioSource audio_fail;
    public AudioSource audio_death;
    public AudioSource audio_win;
    public AudioSource audio_slash;
    public AudioSource audio_avoid;
    public AudioSource audio_spawn;
    public AudioSource audio_critical;
    public AudioSource audio_heal;
    public AudioSource audio_ATKdown;
    public AudioSource audio_pause;
    public AudioSource audio_unpause;
    public AudioSource audio_speedup;
    public AudioSource audio_move;
    public AudioSource audio_select;
    public AudioSource bgm;
    public AudioSource bgm_enemy;
     
    // Start is called before the first frame update
    void Start()
    {
        audio_move.volume=volume_value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
