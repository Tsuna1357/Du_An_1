using UnityEngine;

public enum PuzzleAction
{
    None,

    ActiveObject,
    DeactiveObject,

    AnimatorBool,
    AnimatorInt,

    OpenPopup,
    ClosePopup,

    AddItem,
    RemoveItem,
    ReplaceItem,

    Finish
}