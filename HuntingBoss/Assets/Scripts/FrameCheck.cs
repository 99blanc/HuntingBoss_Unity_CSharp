using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCheck : MonoBehaviour
{
    private float DeltaTime = 0f;

    void Start()
    {
        
    }


    void Update()
    {
        DeltaTime += (Time.unscaledDeltaTime - DeltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int W = Screen.width;
        int H = Screen.height;
        GUIStyle Style = new GUIStyle();
        Rect Rect = new Rect(0, 0, W, H * 2 / 100);
        Style.alignment = TextAnchor.UpperLeft;
        Style.fontSize = H * 2 / 100;
        Style.normal.textColor = new Color(0f, 1f, 0f, 1f);
        float MSec = DeltaTime * 1000.0f;
        float FPS = 1.0f / DeltaTime;
        string Text = string.Format("{0:0.0} ms ({1:0.} FPS)", MSec, FPS);
        GUI.Label(Rect, Text, Style);
    }
}
