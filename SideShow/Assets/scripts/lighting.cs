using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lighting : MonoBehaviour
{
    public int address = 1;

    static int channels = 12;
    public int[] data = new int[channels];

    string hexmap = "0123456789ABCDEF";
    int hi = 0;
    int lo = 0;

    float i1 = 0.0f;
    float r1 = 0.0f;
    float g1 = 0.0f;
    float b1 = 0.0f;
    float i2 = 0.0f;
    float r2 = 0.0f;
    float g2 = 0.0f;
    float b2 = 0.0f;
    float pan = 0.0f;
    float tilt = 0.0f;
    float width = 0.0f;
    float height = 0.0f;

    public void SetData(string data_in)
    {
        for (int i = (address - 1); i < ((address - 1) + channels); ++i)
        {
            hi = hexmap.IndexOf(data_in[(i * 2)]);
            lo = hexmap.IndexOf(data_in[(i * 2) + 1]);
            data[i] = (hi << 4) + lo;
        }

        i1 = (float)data[0] / 255.0f;
        r1 = (float)data[1] / 255.0f;
        g1 = (float)data[2] / 255.0f;
        b1 = (float)data[3] / 255.0f;
        i2 = (float)data[4] / 255.0f;
        r2 = (float)data[5] / 255.0f;
        g2 = (float)data[6] / 255.0f;
        b2 = (float)data[7] / 255.0f;
        pan = 180.0f - (360.0f * ((float)data[8] / 255.0f));
        tilt = 180.0f - (360.0f * ((float)data[9] / 255.0f));
        width =  1.0f - ((float)data[10] / 255.0f);
        height = 1.0f - ((float)data[11] / 255.0f);

        this.GetComponent<Renderer>().materials[0].SetVector("_Color", new Vector4(r1, g1, b1, i1));
        this.GetComponent<Renderer>().materials[0].SetVector("_EmissionColor", new Vector4(r1, g1, b1, i1));
        this.GetComponent<Renderer>().materials[1].SetVector("_Color", new Vector4(r2, g2, b2, i2));
        this.GetComponent<Renderer>().materials[1].SetVector("_EmissionColor", new Vector4(r2, g2, b2, i2));

        this.transform.rotation = Quaternion.Euler(tilt, 180.0f, pan);
        this.transform.localScale = new Vector3(width, height, 1.0f);
    }
}
