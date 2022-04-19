using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : GameController
{
    static int step = 0;
    public static bool game_end = false;
    Subscription<GameEndEvent> end_sub;

    // Start is called before the first frame update
    public override void Start()
    {
        click_sub = EventBus.Subscribe<ClickEvent>(GrowOnClick);
        end_sub = EventBus.Subscribe<GameEndEvent>(Proceed);

        step = 0;
        game_end = false;
        GameController.score = 0;
        depths = new List<List<Branch>> {new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};

        root = Instantiate(branch, new Vector3(0, -4, 0), Quaternion.identity);
        depths[0].Add(root.GetComponent<Branch>());

    }

    // Update is called once per frame
    void Proceed(GameEndEvent e)
    {
        if (step == 0) {
            EventBus.Publish<TutorialProceed>(new TutorialProceed(1));
            StartCoroutine(Pause1()); 
        }
        else if (step == 1)
        {
            EventBus.Publish<TutorialProceed>(new TutorialProceed(2));
            StartCoroutine(Pause2());
        }
        else if (step == 2)
        {
            EventBus.Publish<TutorialProceed>(new TutorialProceed(3));
            StartCoroutine(Pause3());
        }
        else if (step == 3)
        {
            EventBus.Publish<TutorialProceed>(new TutorialProceed(4));
            //StartCoroutine(Pause4());
        }

    }

    IEnumerator Pause1()
    {
        yield return new WaitForSeconds(1f);
        if (root != null) root.GetComponent<Branch>().Delete();
        depths = new List<List<Branch>> {
        new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};

        root = Instantiate(branch, new Vector3(0, -4, 0), Quaternion.identity);
        depths[0].Add(root.GetComponent<Branch>());
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>());
        step = 1;
    }
    IEnumerator Pause2()
    {
        yield return new WaitForSeconds(1f);
        if (root != null) root.GetComponent<Branch>().Delete();
        depths = new List<List<Branch>> {
        new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};
        
        root = Instantiate(branch, new Vector3(0, -4, 0), Quaternion.identity);
        depths[0].Add(root.GetComponent<Branch>());
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>());
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0).GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0).GetChild(1));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0).GetChild(2));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2).GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2).GetChild(1));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2).GetChild(2));
        step = 2;
    }
    IEnumerator Pause3()
    {
        yield return new WaitForSeconds(1f);
        if (root != null) root.GetComponent<Branch>().Delete();
        depths = new List<List<Branch>> {
        new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};
        
        root = Instantiate(branch, new Vector3(0, -4, 0), Quaternion.identity);
        depths[0].Add(root.GetComponent<Branch>());
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>());
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0).GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0).GetChild(1));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(0).GetChild(2));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(1).GetChild(1));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2).GetChild(0));
        TutorialGrow.GrowBranch(branch, root.GetComponent<Branch>().GetChild(2).GetChild(1));
        TutorialGrow.GrowOneBranch(branch, root.GetComponent<Branch>().GetChild(2).GetChild(2), 0);
        step = 3;
    }

    IEnumerator Pause4()
    {
        yield return new WaitForSeconds(1);
        
        root.GetComponent<Branch>().Delete();
    }

    public void ClickReset()
    {
        if (step == 1) StartCoroutine(Pause1());
        else if (step == 2) StartCoroutine(Pause2());
        else if (step == 3) StartCoroutine(Pause3());
    }

    public override void GrowOnClick(ClickEvent e)
    {
        if (step == 3)
        {
            StartCoroutine(GrowDelay());
        }
    }

    IEnumerator GrowDelay()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i=0; i<3; i++)
            {
                Branch grow_branch = TutorialGrow.GetNonFullBranch();
                if (grow_branch == null) game_end = true;
                if (grow_branch != null && !game_end)
                {
                    int empty_child = grow_branch.FindEmptyChild();
                    if (empty_child == 3) game_end = true;
                    TutorialGrow.GrowOneBranch(branch, grow_branch, empty_child);
                }
            }
    }


}

public class TutorialProceed
{
    public int guide_step = 0;
    public TutorialProceed(int _guide_step) {guide_step = _guide_step; }
}
