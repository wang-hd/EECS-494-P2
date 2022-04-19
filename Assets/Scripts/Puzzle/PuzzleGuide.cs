using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGuide : MonoBehaviour
{
    Text guide;
    float spawnTime_1 = 0.5f;
    float spawnTime_2 = 0.1f;
    Vector3 initScale;
    Subscription<Puzzle_1_End> puzzle_solve_sub;
    Subscription<GameEndEvent> game_end_sub;
    Subscription<ResetEvent> reset_sub;


    // Start is called before the first frame update
    void Start()
    {
        guide = GetComponent<Text>();
        initScale = transform.localScale;
        guide.enabled = false;

        puzzle_solve_sub = EventBus.Subscribe<Puzzle_1_End>(ShowGuide);
        game_end_sub = EventBus.Subscribe<GameEndEvent>(GameEndEvent);
        reset_sub = EventBus.Subscribe<ResetEvent>(ResetText);
    }

    void ShowGuide(Puzzle_1_End e)
    {
        guide.enabled = true;
        if (e.status == 1) guide.text = "You Win!";
        else if (e.status == 2) guide.text = "Too much!";
        else if(e.status == 0) guide.text = "Out of steps!";
        StartCoroutine(DropInEffect());
    }
    void GameEndEvent(GameEndEvent e) 
    {
        if (guide.enabled == true) return;
        guide.enabled = true;
        guide.text = "No more valid moves!";
        StartCoroutine(DropInEffect());
    }
    void ResetText(ResetEvent e) 
    {
        guide.enabled = false;
    }

    IEnumerator DropInEffect()
    {
        float i = 0.0f;
        float rate = 1.0f / spawnTime_1;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(Vector3.zero, initScale * 1.2f, i);
            yield return new WaitForEndOfFrame();
        }
        i = 0.0f;
        rate = 1.0f / spawnTime_2;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(initScale * 1.2f, initScale, i);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy() {
        EventBus.Unsubscribe(puzzle_solve_sub);
        EventBus.Unsubscribe(reset_sub);
        EventBus.Unsubscribe(game_end_sub);
    }
}
