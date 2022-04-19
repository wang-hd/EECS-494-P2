using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    Text game_over_text;
    Text score_text;
    Text guide_text;
    Subscription<GameEndEvent> game_end_sub;
    Subscription<ScoreEvent> score_sub;
    Subscription<WrongClickEvent> wrong_click_sub;

    // Start is called before the first frame update
    void Start()
    {
        game_over_text = GameObject.Find("GameOverText").GetComponent<Text>();
        score_text = GameObject.Find("ScoreText").GetComponent<Text>();
        guide_text = GameObject.Find("GuideText").GetComponent<Text>();

        game_end_sub = EventBus.Subscribe<GameEndEvent>(GameEnd);
        score_sub = EventBus.Subscribe<ScoreEvent>(Scored);
        wrong_click_sub = EventBus.Subscribe<WrongClickEvent>(ShowGuide);

        game_over_text.enabled = false;
    }

    void GameEnd(GameEndEvent e)
    {
        game_over_text.enabled = true;
        if (e.win) game_over_text.text = "You Win!";
        else game_over_text.text = "Game Over";
    }

    void Scored(ScoreEvent e)
    {
        score_text.text = "Broccoil Collected: " + e.new_score;
        guide_text.text = "Collect all broccolis to win!";
    }

    void ShowGuide(WrongClickEvent e)
    {
        guide_text.text = "Oops, that is not a *perfect* broccoli!";
    }

    private void OnDestroy() {
        EventBus.Unsubscribe(game_end_sub);
        EventBus.Unsubscribe(score_sub);
    }
}
