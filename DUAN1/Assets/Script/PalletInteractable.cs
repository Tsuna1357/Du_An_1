using UnityEngine;

public class PalletInteractable : Interactable
{
    public PalletColorPuzzle palletColorPuzzle;

    public override void OnClick()
    {
        palletColorPuzzle.CheckItem();
    }
}