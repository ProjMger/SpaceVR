using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GazeCircle : MonoBehaviour
{
    public Image imgCircle;
    public float fillTime = 1.5f;

    bool gvrStatus;
    float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;

    void Update()
    {
        if(gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / fillTime;
        }
        else
        {
            gvrTimer = 0f;
            imgCircle.fillAmount = 0f;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, distanceOfRay) && imgCircle.fillAmount >= 1f)
        {
            gvrTimer = 0f;
            GVROff();
            _hit.transform.SendMessage("GazeClick");
        }

    }


    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
    }

}
