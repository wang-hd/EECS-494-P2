using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject branch;
    public static int grow_quantity = 1;
    public static int score = 0;
    public static bool game_over = false;
    protected GameObject root;
    protected Subscription<ClickEvent> click_sub;
    protected Subscription<GameEndEvent> game_end_sub;

    public static List<List<Branch>> depths = new List<List<Branch>> {
        new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()
    };


    public virtual void Start()
    {
        click_sub = EventBus.Subscribe<ClickEvent>(GrowOnClick);
        game_end_sub = EventBus.Subscribe<GameEndEvent>(SetGameEnd);

        score = 0;
        game_over = false;
        grow_quantity = 1;
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


    public virtual void GrowOnClick(ClickEvent e)
    {
        if (!game_over)
        {
            if (score < 10) grow_quantity = 1;
            else if (score < 20) grow_quantity = 2;
            else if (score < 30) grow_quantity = 3;
            else if (score < 40) grow_quantity = 4;
            else grow_quantity = 5;
            StartCoroutine(GrowDelay());
        }
    }

    IEnumerator GrowDelay()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i=0; i<grow_quantity; i++)
            {
                if (!game_over)
                {
                    Branch grow_branch = Grow.GetNonFullBranch();
                    if (grow_branch != null)
                    {
                        int empty_child = grow_branch.FindEmptyChild();
                        if (empty_child == 3) {
                            //Debug.Log(" Failed to grow branch depth: "+ (grow_branch.GetDepth()+1).ToString() + ", child" + empty_child);
                            continue;
                        }
                        Grow.GrowOneBranch(branch, grow_branch, empty_child);
                        //Debug.Log("Grow branch depth: "+ (grow_branch.GetDepth()+1).ToString() + ", child" + empty_child);
                    }
                }
                
            }
    }

    public virtual void SetGameEnd(GameEndEvent e) 
    {
        game_over = true;
    }

    public virtual void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    private void OnDestroy() {
        EventBus.Unsubscribe(click_sub);
        EventBus.Unsubscribe(game_end_sub);
    }
}
