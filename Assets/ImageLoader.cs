using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ImageLoader  {

    MyDelegate cb;
    bool alive = true;
    string loadImageName = "";

    public void loadImageIntoFrame(string imageName, int imageFrame)
    {
        loadImageName = imageName;
    }

    public void stop()
    {
        alive = false;
    }

    public void setup(MyDelegate callback)
    {
        cb = callback;
    }

    public void run()
    {
        while (alive)
        {
            cb(loadImageName);
        }
    }
}


