using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : GameController
{
    Subscription<TestClickEvent> test_click_sub;

    // Start is called before the first frame update
    public override void Start()
    {
        test_click_sub = EventBus.Subscribe<TestClickEvent>(TestGrowOnClick);
        game_end_sub = EventBus.Subscribe<GameEndEvent>(SetGameEnd);

        score = 0;
        game_over = false;
        grow_quantity = 1;
        root = Instantiate(branch, new Vector3(0, -3, 0), Quaternion.identity);

        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>());
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(0));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(2));

        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1).GetChild(0));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1).GetChild(1));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1).GetChild(2));
    }

    public override void GrowOnClick(ClickEvent e)
    {
        return;
    }

    public void TestGrowOnClick(TestClickEvent e) 
    {
        if (!game_over)
        {
            StartCoroutine(TestGrowDelay(e.parent, e.child));
        }
    }

    IEnumerator TestGrowDelay(TestBranch testbranch, int i)
    {
        yield return new WaitForSeconds(0.3f);
        List<KeyValuePair<TestBranch, int>> siblings = testbranch.GetChild(i).GetSiblings();
        foreach (KeyValuePair<TestBranch, int> b in siblings)
        {
            int empty_child = b.Value;
            if (empty_child == 3) continue;
            TestGrow.GrowOneBranch(branch, b.Key, empty_child);
        }
    }

    public void ClickReset()
    {
        if (root != null) root.GetComponent<TestBranch>().Delete();
        TestGuided.step = 0;

        root = Instantiate(branch, new Vector3(0, -3, 0), Quaternion.identity);

        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>());
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(0));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(2));

        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1).GetChild(0));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1).GetChild(1));
        TestGrow.GrowBranch(branch, root.GetComponent<TestBranch>().GetChild(1).GetChild(2));
    }

    
}
