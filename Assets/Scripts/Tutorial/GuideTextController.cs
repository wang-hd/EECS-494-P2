using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideTextController : MonoBehaviour
{
    float fadeTime = 3f;
    Text guide;
    Subscription<TutorialProceed> tutorial_sub;
    // Start is called before the first frame update
    void Start()
    {
        guide = GetComponent<Text>();
        tutorial_sub = EventBus.Subscribe<TutorialProceed>(ShowGuide);

        guide.text = "Welcome to Brocccli!\n Click on a broccoli to harvest it";
    }

    void ShowGuide(TutorialProceed e)
    {
        if (e.guide_step == 1) 
        {
            guide.text = "Good Job!";
            StartCoroutine(Step1Wait());
        }
        if (e.guide_step == 2) 
        {
            guide.text = "Good Job!";
            StartCoroutine(Step2Wait());
        }
        if (e.guide_step == 3) 
        {
            guide.text = "Good Job!";
            StartCoroutine(Step3Wait());
        }
        if (e.guide_step == 4)
        {
            guide.text = "Congratuations! You have finished tutorial";
        }
    }

    IEnumerator Step1Wait()
    {
        yield return new WaitForSeconds(1);
        guide.text = "A broccoli with 0 or full childs is Perfect.\n Click on a perfect broccoli to harvest it";
    }
    IEnumerator Step2Wait()
    {
        yield return new WaitForSeconds(1);
        guide.text = "If all branches are perfect (empty or full), the broccoli is perfect.\n This is also a perfect broccoli";
    }
    IEnumerator Step3Wait()
    {
        yield return new WaitForSeconds(1);
        guide.text = "If broccoli tree's 4 layers are full, you will lose the game";
    }

    IEnumerator FadeInEffect()
    {
        Color initColor = guide.color;
        guide.color = new Color(initColor.r, initColor.g, initColor.b, 0f);
        float i = 0.0f;
        float rate = 1.0f / fadeTime;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            guide.color = new Color(initColor.r, initColor.g, initColor.b, guide.color.a + i);
            yield return new WaitForEndOfFrame();
        }
        
    }
}
