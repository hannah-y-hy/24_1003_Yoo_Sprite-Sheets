using UnityEngine;

public class Example
{
    void Start()
    {
        //  For PC, Mac, Mobile: Let the device determine the framerate
        //  WebGL Builds: let the browser set the frame rate 
        Application.targetFrameRate = -1;   // -1 is a magic number
    }
}


