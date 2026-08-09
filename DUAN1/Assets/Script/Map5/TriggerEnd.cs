using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public enum EndingType
    {
        Ending1,
        Ending2
    }

    [Header("Ending")]
    public EndingType endingType;

    public EndingControler endingManager;

    private void OnMouseDown()
    {
        if (endingManager == null)
        {
            Debug.LogError("Chưa gán EndingManager!");
            return;
        }

        // Đang ở Ending → không nhận click
        if (endingManager.endingPlaying)
        {
            return;
        }

        if (endingType == EndingType.Ending1)
        {
            endingManager.ShowEnding1();
        }
        else if (endingType == EndingType.Ending2)
        {
            endingManager.ShowEnding2();
        }
    }
}