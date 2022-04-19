using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrow : MonoBehaviour
{
    public static void GrowOneBranch(GameObject branch, Branch start, int i)
    {
        Vector3 growPos = start.transform.position;
        growPos = start.transform.position + GetDirection(start) * 1.3f * start.transform.localScale.x;
        GameObject new_branch = null;

        if (i == 0)
        {
            new_branch = Instantiate(branch, growPos, start.transform.rotation * Quaternion.Euler(0, 0, 70));
        }
        else if (i == 1)
        {
            new_branch = Instantiate(branch, growPos, start.transform.rotation);
        }
        else if (i == 2)
        {
            new_branch = Instantiate(branch, growPos, start.transform.rotation * Quaternion.Euler(0, 0, -70));
        }
        
        new_branch.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(new_branch.GetComponent<Branch>());

        int new_depth = start.GetDepth() + 1;
        new_branch.GetComponent<Branch>().SetDepth(new_depth);
        TutorialController.depths[new_depth].Add(new_branch.GetComponent<Branch>());
    }

    // Input a start branch, grow 3 child branch on this branch
    public static void GrowBranch(GameObject branch, Branch start)
    {
        Vector3 growPos = start.transform.position;

        growPos = start.transform.position + GetDirection(start) * 1.3f * start.transform.localScale.x;
        
        GameObject left = Instantiate(branch, growPos, start.transform.rotation * Quaternion.Euler(0, 0, 70));
        left.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(left.GetComponent<Branch>());
        int new_depth = start.GetDepth() + 1;
        left.GetComponent<Branch>().SetDepth(new_depth);
        TutorialController.depths[new_depth].Add(left.GetComponent<Branch>());

        GameObject middle = Instantiate(branch, growPos, start.transform.rotation);
        middle.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(middle.GetComponent<Branch>());
        middle.GetComponent<Branch>().SetDepth(new_depth);
        TutorialController.depths[new_depth].Add(middle.GetComponent<Branch>());

        GameObject right = Instantiate(branch, growPos, start.transform.rotation * Quaternion.Euler(0, 0, -70));
        right.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(right.GetComponent<Branch>());
        right.GetComponent<Branch>().SetDepth(new_depth);
        TutorialController.depths[new_depth].Add(right.GetComponent<Branch>());
    }

    public static Vector3 GetDirection(Branch start)
    {
        float angle = start.transform.rotation.eulerAngles.z;
        float tan = Mathf.Tan(-angle * Mathf.Deg2Rad);

        //Debug.Log("z = "+ angle.ToString() + ", tan = " + tan.ToString());
        
        //Debug.Log(new Vector3 (tan, 1, 0).normalized.ToString());
        if (angle > 90 && angle < 270) return new Vector3 (-tan, -1, 0).normalized;
        return new Vector3 (tan, 1, 0).normalized;
    }

    public static Branch GetNonFullBranch()
    {
        int target = 0;
        if (TutorialController.depths[1].Count < 3) return TutorialController.depths[0][0].GetComponent<Branch>();
        else if (TutorialController.depths[2].Count < 9) target = 1;
        else if (TutorialController.depths[3].Count < 27) target = 2;
        else 
        {
            Debug.Log(TutorialController.depths[0].Count.ToString() + ", " + TutorialController.depths[1].Count.ToString() + ", " 
            + TutorialController.depths[2].Count.ToString() + ", " + TutorialController.depths[3].Count.ToString() + ", " );
            // End game if nowhere to grow new branch
            EventBus.Publish<GameEndEvent>(new GameEndEvent(false));
            TutorialController.game_end = true;
        }
        foreach (Branch b in TutorialController.depths[target])
        {
            if (!b.isFull()) return b;
        }
        return null;
    }
}
