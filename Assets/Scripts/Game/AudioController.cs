using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip pick_sound;
    public AudioClip error_sound;
    public AudioClip win_sound;
    public AudioClip lose_sound;
    Subscription<ClickEvent> click_sub;
    Subscription<WrongClickEvent> error_sub;
    Subscription<GameEndEvent> game_end_sub;
    // Start is called before the first frame update
    void Start()
    {
        click_sub = EventBus.Subscribe<ClickEvent>(PlayPickSound);
        error_sub = EventBus.Subscribe<WrongClickEvent>(PlayErrorSound);
        game_end_sub = EventBus.Subscribe<GameEndEvent>(PlayEndSound);
    }

    // Update is called once per frame
    void PlayPickSound(ClickEvent e)
    {
        AudioSource.PlayClipAtPoint(pick_sound, Camera.main.transform.position);
    }
    void PlayErrorSound(WrongClickEvent e)
    {
        AudioSource.PlayClipAtPoint(error_sound, Camera.main.transform.position);
    }
    void PlayEndSound(GameEndEvent e)
    {
        if (e.win) AudioSource.PlayClipAtPoint(win_sound, Camera.main.transform.position);
        else AudioSource.PlayClipAtPoint(lose_sound, Camera.main.transform.position);
    }


    private void OnDestroy() {
        EventBus.Unsubscribe(click_sub);
        EventBus.Unsubscribe(error_sub);
    }
}
