using UnityEngine;

public class TutorialCloth : Interactable
{
    public Map1Tutorial tutorial;

    public override void OnClick()
    {
        if (tutorial != null)
        {
            tutorial.ClickCloth();
        }
    }
}