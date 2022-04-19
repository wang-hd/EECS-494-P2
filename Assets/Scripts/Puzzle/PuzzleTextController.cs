using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTextController : MonoBehaviour
{
    Text score_text;
    Text goal_text;
    Text step_text;
    Subscription<ScoreEvent> score_sub;
    Subscription<ResetEvent> reset_sub;
    Subscription<NewGameEvent> new_game_sub;
    // Start is called before the first frame update
    void Start()
    {
        score_text = GameObject.Find("Score").GetComponent<Text>();
        goal_text = GameObject.Find("GoalNumber").GetComponent<Text>();
        step_text = GameObject.Find("StepNumber").GetComponent<Text>();

        score_sub = EventBus.Subscribe<ScoreEvent>(ShowScore);
        score_sub = EventBus.Subscribe<ScoreEvent>(ShowStep);
        reset_sub = EventBus.Subscribe<ResetEvent>(ResetText);
        new_game_sub = EventBus.Subscribe<NewGameEvent>(NewGameText);
    }

    private void Update() {
        goal_text.text = Puzzle_1_Controller.answer.ToString();
    }

    void ShowScore(ScoreEvent e)
    {
        score_text.text = "Broccoli collected: " + e.new_score.ToString();
    }
    void ShowStep(ScoreEvent e) 
    {
        step_text.text = "Remaining clicks: " + Puzzle_1_Controller.steps.ToString();
    }
    void SetGoal(SetGoalEvent e)
    {
        goal_text.text = e.goal.ToString();
        Debug.Log("Set goal number to: " + e.goal);
    }
    void ResetText(ResetEvent e)
    {
        score_text.text = "Broccoli collected: 0";
    }
    void NewGameText(NewGameEvent e) 
    {
        //goal_text.text = Puzzle_1_Controller.answer.ToString();
    }

    private void OnDestroy() {
        EventBus.Unsubscribe(score_sub);
        EventBus.Unsubscribe(reset_sub);
        EventBus.Unsubscribe(new_game_sub);
    }
}
