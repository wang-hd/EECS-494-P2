using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGuided : MonoBehaviour
{
    Text guide;
    public static int step = 0;
    Subscription<TestClickEvent> test_click_sub;
    readonly Dictionary<int, string> guide_dict = new Dictionary<int, string>() {
        {0, "This page is under construction."}, 
        {1, "The rules are similar: click on a perfect broccoli to collect it"},
        {2, ", and collect all broccolis to win."},
        {3, "But the broccolis grow in a different way,"},
        {4, "When you click on a broccoli, a new broccoli grows on each *adjacent* siblings"},
        {5, ", or grow a new sibling if that position is empty."},
        {6, "This puzzle is too difficult that I myself can't solve it"},
        {7, "Actually I'm not sure if this has a solution"},
        {8, "But this is originally how I design this game so I kept it"},
        {9, "Btw you are now not limited by the 4 layers limitaion"},
        {10, "Now feel free to have a try and enjoy the fractal vegetable!"},
        {11, "P.S. Click on the root will let you win instantly, but not recommended"},
        {12, "Good Luck!"},
        {13, ""}
    };
    // Start is called before the first frame update
    void Start()
    {
        guide = GetComponent<Text>();
        test_click_sub = EventBus.Subscribe<TestClickEvent>(ShowGuide);
    }

    void ShowGuide(TestClickEvent e) 
    {
        step++;
        if (step >= 13) step = 13;
        guide.text = guide_dict[step];
    }
}
