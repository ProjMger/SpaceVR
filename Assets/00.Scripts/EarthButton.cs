using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EarthButton : MonoBehaviour
{
    public PlayableDirector director;
    void GazeClick()
    {
        director.Play();
    }
}
