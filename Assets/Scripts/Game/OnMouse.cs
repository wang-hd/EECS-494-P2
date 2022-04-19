using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
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
            Branch branch = gameObject.GetComponent<Branch>();
            if (branch.isPerfect())
            {
                if (branch.GetDepth() == 0) EventBus.Publish<GameEndEvent>(new GameEndEvent(true));
                branch.Delete();
                EventBus.Publish<ClickEvent>(new ClickEvent());
                EventBus.Publish<ScoreEvent>(new ScoreEvent(GameController.score));    
                
            }
            else EventBus.Publish<WrongClickEvent>(new WrongClickEvent());
        }
    }
}
public class ClickEvent {public ClickEvent() {} }

public class WrongClickEvent {public WrongClickEvent() {} }

public class ScoreEvent
{
    public int new_score = 0;
    public ScoreEvent(int _new_score) { new_score = _new_score; }

}

public class GameEndEvent 
{
    public bool win = false;
    public GameEndEvent(bool _win) { win = _win; } 
}