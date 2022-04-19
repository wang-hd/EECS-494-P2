using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    bool isHovering = false;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && isHovering)
        {
            TestBranch branch = gameObject.GetComponent<TestBranch>();
            if (branch.isPerfect())
            {
                TestBranch parent = branch.parent;
                int i = 3;
                if (branch == branch.parent.GetChild(0)) i = 0;
                else if (branch == branch.parent.GetChild(1)) i = 1;
                else if (branch == branch.parent.GetChild(2)) i = 2;

                if (branch.GetDepth() == 0) EventBus.Publish<GameEndEvent>(new GameEndEvent(true));
                branch.Delete();
                EventBus.Publish<TestClickEvent>(new TestClickEvent(parent, i));
                EventBus.Publish<ClickEvent>(new ClickEvent());
                EventBus.Publish<ScoreEvent>(new ScoreEvent(GameController.score));

                // for test level
                
                
            }
            else EventBus.Publish<WrongClickEvent>(new WrongClickEvent());
        }
    }
}

// for test level
public class TestClickEvent
{
    public TestBranch parent;
    public int child;
    public TestClickEvent(TestBranch _parent, int _child) {parent = _parent; child = _child;}
}