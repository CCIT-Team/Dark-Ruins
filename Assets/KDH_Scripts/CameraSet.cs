using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    void Awake()
    {
        float targetAspect = 16f / 9f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scale = windowAspect / targetAspect;

        Camera cam = Camera.main;

        if (scale < 1.0f)
        {
            // 좌우는 그대로, 상하만 잘라짐 → 검은 영역
            float height = scale;
            cam.rect = new Rect(0, (1 - height) / 2, 1, height);
        }
        else
        {
            // 상하는 그대로, 좌우 잘림
            float width = 1 / scale;
            cam.rect = new Rect((1 - width) / 2, 0, width, 1);
        }

    }
}
