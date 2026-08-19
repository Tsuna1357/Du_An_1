using UnityEngine;

public class GapHacStep : Interactable
{
    public GapHac gapHac;

    public int stepID;

    public override void OnClick()
    {
        if (gapHac == null)
            return;

        gapHac.ClickStep(stepID);

        
    }
}