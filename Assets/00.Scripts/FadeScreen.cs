using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;
    public Renderer reticlePointer;

    float fadeAlpha;
    bool buttonClicked;
    bool fadeOut;

    void GazeClick()
    {
        buttonClicked = true;
    }

    IEnumerator FadeAction()
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if(buttonClicked)
        {
            if (!fadeOut)
            {
                fadeAlpha += 0.5f * Time.deltaTime;
                fadeImage.color = new Vector4(0f, 0f, 0f, fadeAlpha);
                reticlePointer.material.color = new Vector4 (255f, 255f, 255f, 1f - fadeAlpha);
                if(fadeAlpha >= 1.5f)
                {
                    StartCoroutine("FadeAction");
                    fadeOut = true;
                }
            }
            else
            {
                fadeAlpha -= 0.5f * Time.deltaTime;
                fadeImage.color = new Vector4(0f, 0f, 0f, fadeAlpha);
                reticlePointer.material.color = new Vector4(255f, 255f, 255f, 1f - fadeAlpha);
                if (fadeAlpha <= 0f)
                {
                    buttonClicked = false;
                    fadeOut = false;
                }
            }
        }
    }
}
