using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class SceneController : MonoBehaviour, IPointerDownHandler
{
    Collider scene_collider;
    bool game_end = false;
    Subscription<GameEndEvent> game_end_sub;
    // Start is called before the first frame update
    void Start()
    {
        game_end_sub = EventBus.Subscribe<GameEndEvent>(WaitForPlayerInput);
        scene_collider = GetComponent<BoxCollider>();
        scene_collider.enabled = false;
    }

    void WaitForPlayerInput(GameEndEvent e)
    {
        game_end = true;
        scene_collider.enabled = true;
        
        Debug.Log("game over: " + game_end);
    }

    /*IEnumerator ResetGame()
    {
        GameController.depths = new List<List<Branch>> {new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("game end: " + game_end);
        if (game_end && eventData.button == PointerEventData.InputButton.Left)
        {
            GameController.depths = new List<List<Branch>> {new List<Branch>(), new List<Branch>(), new List<Branch>(), new List<Branch>()};
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDestroy() {
        EventBus.Unsubscribe(game_end_sub);
    }
    
}
