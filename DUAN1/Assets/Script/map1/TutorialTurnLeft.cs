using UnityEngine;

public class TutorialTurnLeft : MonoBehaviour
{
    public CameraMap1 cameraMap;
    public Map1Tutorial tutorial;

    public void Click()
    {
        cameraMap.RotateLeft();

        if (tutorial.currentState ==
            Map1Tutorial.TutorialState.Turn)
        {
            tutorial.TurnLeft();
        }
        else if (tutorial.currentState ==
                 Map1Tutorial.TutorialState.TurnBack)
        {
            tutorial.TurnBack();
        }
    }
}