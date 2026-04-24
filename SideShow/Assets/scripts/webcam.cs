using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class webcam : MonoBehaviour
{
    public Material output1 = null;
    public Material output2 = null;
    public Material output3 = null;

    int captureWidth = 1280;
    int captureHeight = 720;
    float captureFPS = 59.94f;
    int captureSelect = 0;

    public void Start() { SelectDevice(captureSelect); }

    public void SelectDevice(int selected = 0)
    {
        captureSelect = selected;
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("[INFO]: Selected: " + selected.ToString() + ": " + devices[selected].name);

        WebCamTexture webcam = new WebCamTexture(devices[selected].name);

        output1.SetTexture("_MainTex", webcam);
        output1.SetTextureScale("_MainTex", new Vector2(1.0f, 1.0f));
        output1.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.0f));

        output1.SetTexture("_EmissionMap", webcam);
        output1.SetTextureScale("_EmissionMap", new Vector2(1.0f, 1.0f));
        output1.SetTextureOffset("_EmissionMap", new Vector2(0.0f, 0.0f));

        output2.SetTexture("_MainTex", webcam);
        output2.SetTextureScale("_MainTex", new Vector2(1.0f, 1.0f));
        output2.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.0f));

        output2.SetTexture("_EmissionMap", webcam);
        output2.SetTextureScale("_EmissionMap", new Vector2(1.0f, 1.0f));
        output2.SetTextureOffset("_EmissionMap", new Vector2(0.0f, 0.0f));

        output3.SetTexture("_MainTex", webcam);
        output3.SetTextureScale("_MainTex", new Vector2(1.0f, 1.0f));
        output3.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.0f));

        output3.SetTexture("_EmissionMap", webcam);
        output3.SetTextureScale("_EmissionMap", new Vector2(1.0f, 1.0f));
        output3.SetTextureOffset("_EmissionMap", new Vector2(0.0f, 0.0f));

        webcam.wrapMode = TextureWrapMode.Repeat;
        webcam.filterMode = FilterMode.Point;
        webcam.requestedWidth = captureWidth;
        webcam.requestedHeight = captureHeight;
        webcam.requestedFPS = captureFPS;

        webcam.Play();
    }
}
