using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    Branch leftChild;
    Branch middleChild;
    Branch rightChild;
    GameObject thisBranch;
    int depth = 0;
    float spawnTime_1 = 0.25f;
    float spawnTime_2 = 0.05f;
    Vector3 initLocalScale;
    // Start is called before the first frame update
    void Start() {
        initLocalScale = transform.localScale;
        StartCoroutine(ScaleAtSpawn());
    }
    
    IEnumerator ScaleAtSpawn()
    {
        float i = 0.0f;
        float rate = 1.0f / spawnTime_1;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(Vector3.zero, initLocalScale * 1.2f, i);
            yield return new WaitForEndOfFrame();
        }
        i = 0.0f;
        rate = 1.0f / spawnTime_2;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(initLocalScale * 1.2f, initLocalScale, i);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ScaleAtDelete()
    {
        float i = 0.0f;
        float rate = 1.0f / spawnTime_1;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            //transform.localScale = Vector3.Lerp(initLocalScale, initLocalScale * 1.2f, i);
            transform.position = Vector3.Lerp(transform.position, transform.position + GetDirection() * 0.01f * transform.localScale.x, i);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    public bool isFull()
    {
        return leftChild != null && middleChild != null && rightChild != null;
    }

    public bool isEmpty()
    {
        return leftChild == null && middleChild == null && rightChild == null;
    }

    public bool isPerfect()
    {
        if (this.isEmpty()) return true;
        else if (this.isFull()) return leftChild.isPerfect() && middleChild.isPerfect() && rightChild.isPerfect();
        else return false;
    }

    public int FindEmptyChild()
    {
        if (leftChild == null) return 0;
        else if (middleChild == null) return 1;
        else if (rightChild == null) return 2;
        else return 3;
    }

    public void SetDepth(int new_depth)
    {
        depth = new_depth;
    }

    public int GetDepth()
    {
        return depth;
    }

    public GameObject GetGameObject()
    {
        return thisBranch;
    }

    public Branch GetChild(int n)
    {
        switch(n)
        {
            case 0:
                return leftChild;
            case 1:
                return middleChild;
            case 2:
                return rightChild;
            default:
                return null;
        }
    }

    public Vector3 GetDirection()
    {
        float angle = transform.rotation.eulerAngles.z;
        float tan = Mathf.Tan(-angle * Mathf.Deg2Rad);

        if (angle > 90 && angle < 270) return new Vector3 (-tan, -1, 0).normalized;
        return new Vector3 (tan, 1, 0).normalized;
    }

    public void Insert(Branch child)
    {
        if (leftChild == null) leftChild = child;
        else if (middleChild == null) middleChild = child;
        else if (rightChild == null) rightChild = child;
    }

    public void Delete()
    {
        if (leftChild != null) leftChild.Delete();
        if (middleChild != null) middleChild.Delete();
        if (rightChild != null) rightChild.Delete();

        //GameController.score += (4 - this.depth);
        GameController.score ++;
        GameController.depths[this.depth].Remove(this);
        StartCoroutine(ScaleAtDelete());
        
    }

}
