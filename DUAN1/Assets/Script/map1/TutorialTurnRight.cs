using UnityEngine;

public class TutorialTurnRight : MonoBehaviour
{
    public CameraMap1 cameraMap;
    public Map1Tutorial tutorial;

    public void Click()
    {
        cameraMap.RotateRight();

        if (tutorial.currentState ==
            Map1Tutorial.TutorialState.Turn)
        {
            tutorial.TurnRight();
        }
        else if (tutorial.currentState ==
                 Map1Tutorial.TutorialState.TurnBack)
        {
            tutorial.TurnBack();
        }
    }
}