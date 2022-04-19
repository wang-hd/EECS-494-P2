using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle_1_Controller : GameController
{
    public static int answer;
    public static int steps = 10;
    Subscription<ScoreEvent> score_sub;


    public override void Start()
    {
        click_sub = EventBus.Subscribe<ClickEvent>(GrowOnClick);
        score_sub = EventBus.Subscribe<ScoreEvent>(CheckSolve);

        score = 0;
        steps = 10;
        answer = Random.Range(35, 55);
        EventBus.Publish<SetGoalEvent>(new SetGoalEvent(answer));
        game_over = false;
        GameController.score = 0;
        depths = new List<List<Branch>> {
        new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};

        root = Instantiate(branch, new Vector3(0, -3, 0), Quaternion.identity);
        depths[0].Add(root.GetComponent<Branch>());

        Grow.GrowBranch(branch, root.GetComponent<Branch>());
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2));

        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(0));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(1));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(2));
    }

    void CheckSolve(ScoreEvent e)
    {
        steps--;
        // success to solve
        if (e.new_score == answer && steps >= 0)
        {
            EventBus.Publish<Puzzle_1_End>(new Puzzle_1_End(1));
            EventBus.Publish<GameEndEvent>(new GameEndEvent(true));
            return;
        }
        // too much
        else if (e.new_score > answer)
        {
            EventBus.Publish<Puzzle_1_End>(new Puzzle_1_End(2));
            EventBus.Publish<GameEndEvent>(new GameEndEvent(false));
            return;
        }
        // run out of steps
        else if (steps <= 0)
        {
            EventBus.Publish<Puzzle_1_End>(new Puzzle_1_End(0));
            EventBus.Publish<GameEndEvent>(new GameEndEvent(false));
            return;
        }

    }

    public void NewGame()
    {
        SceneManager.LoadScene("Puzzle_1");
    }

    public void ClickReset()
    {
        if (root != null) root.GetComponent<Branch>().Delete();
        score = 0;
        steps = 10;
        game_over = false;
        GameController.score = 0;
        depths = new List<List<Branch>> {
        new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};

        root = Instantiate(branch, new Vector3(0, -3, 0), Quaternion.identity);
        depths[0].Add(root.GetComponent<Branch>());
        Grow.GrowBranch(branch, root.GetComponent<Branch>());
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2));

        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(0));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(1));
        Grow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(2));

        EventBus.Publish<ResetEvent>(new ResetEvent());
    }

}

public class Puzzle_1_End { 
    public int status = 0;
    public Puzzle_1_End(int _status) {status = _status;} 
}

public class SetGoalEvent {
    public int goal = 0;
    public SetGoalEvent(int _goal) {goal = _goal;}
}

public class ResetEvent {public ResetEvent() {}}

public class NewGameEvent {public NewGameEvent() {}}
