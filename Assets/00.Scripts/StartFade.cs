using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{
    public Image fadeImage;
    float fadeAlpha = 1f;

    private void Start()
    {
        fadeImage.color = new Vector4(0f, 0f, 0f, 1f);
        StartCoroutine("ScreenStart");
    }

    IEnumerator ScreenStart()
    {
        yield return new WaitForSeconds(1f);
        while (fadeImage.color.a > 0f)
        {
            fadeAlpha -= 0.5f * Time.deltaTime;
            fadeImage.color = new Vector4(0f, 0f, 0f, fadeAlpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
