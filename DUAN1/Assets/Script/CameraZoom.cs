using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform normalView;
    public Transform targetView;

    public float speed = 3f;

    bool focus;


    void Update()
    {
        Transform target = focus ? targetView : normalView;


        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            Time.deltaTime * speed
        );


        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            Time.deltaTime * speed
        );
    }


    public void Focus()
    {
        focus = true;
    }


    public void Back()
    {
        focus = false;
    }
}
