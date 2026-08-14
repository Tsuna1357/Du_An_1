using UnityEngine;

public class TutorialScissors : Interactable
{
    public Map1Tutorial tutorial;

    public override void OnClick()
    {
        if (tutorial != null)
        {
            tutorial.PickUpScissors();
        }
    }
}