using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPathCreator : MonoBehaviour
{
    [HideInInspector]
    public RailPath path;

    public Color anchorColor = Color.red;
    public Color controlColor = Color.white;
    public Color segmentColor = Color.green;
    public Color selectedSegmentColor = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    public void CreatePath()
    {
        path = new RailPath(transform.position);
    }

    private void Reset()
    {
        CreatePath();
    }
}
