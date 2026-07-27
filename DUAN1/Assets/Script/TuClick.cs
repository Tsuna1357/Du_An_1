using UnityEditor.Rendering;
using UnityEngine;

public class TuClick : MonoBehaviour
{
    public CameraZoom cameraFocus;


    void OnMouseDown()
    {
        cameraFocus.Focus();
    }
}
