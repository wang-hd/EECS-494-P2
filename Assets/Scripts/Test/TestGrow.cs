using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrow : MonoBehaviour
{
    public static void GrowOneBranch(GameObject branch, TestBranch start, int i)
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
        start.Insert(new_branch.GetComponent<TestBranch>(), i);
        new_branch.GetComponent<TestBranch>().parent = start; 

        int new_depth = start.GetDepth() + 1;
        new_branch.GetComponent<TestBranch>().SetDepth(new_depth);
    }

    // Input a start branch, grow 3 child branch on this branch
    public static void GrowBranch(GameObject branch, TestBranch start)
    {
        Vector3 growPos = start.transform.position;

        growPos = start.transform.position + GetDirection(start) * 1.3f * start.transform.localScale.x;
        
        GameObject left = Instantiate(branch, growPos, start.transform.rotation * Quaternion.Euler(0, 0, 70));
        left.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(left.GetComponent<TestBranch>(), 0);
        left.GetComponent<TestBranch>().parent = start; 
        int new_depth = start.GetDepth() + 1;
        left.GetComponent<TestBranch>().SetDepth(new_depth);

        GameObject middle = Instantiate(branch, growPos, start.transform.rotation);
        middle.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(middle.GetComponent<TestBranch>(), 1);
        middle.GetComponent<TestBranch>().parent = start; 
        middle.GetComponent<TestBranch>().SetDepth(new_depth);

        GameObject right = Instantiate(branch, growPos, start.transform.rotation * Quaternion.Euler(0, 0, -70));
        right.transform.localScale = start.transform.localScale * 3 / 5;
        start.Insert(right.GetComponent<TestBranch>(), 2);
        right.GetComponent<TestBranch>().parent = start; 
        right.GetComponent<TestBranch>().SetDepth(new_depth);
    }

    public static Vector3 GetDirection(TestBranch start)
    {
        float angle = start.transform.rotation.eulerAngles.z;
        float tan = Mathf.Tan(-angle * Mathf.Deg2Rad);

        //Debug.Log("z = "+ angle.ToString() + ", tan = " + tan.ToString());
        
        //Debug.Log(new Vector3 (tan, 1, 0).normalized.ToString());
        if (angle > 90 && angle < 270) return new Vector3 (-tan, -1, 0).normalized;
        return new Vector3 (tan, 1, 0).normalized;
    }

    public static TestBranch GetNonFullBranch(TestBranch branch)
    {

        return null;
    }
}
