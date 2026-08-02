using UnityEngine;

[System.Serializable]
public class PuzzleActionData
{
    [Header("Action")]
    public PuzzleAction action;

    [Header("Object")]
    public GameObject targetObject;

    [Header("Animator")]
    public Animator animator;
    public string boolName;
    public bool boolValue;

    public string intName;
    public int intValue;

    [Header("Item")]
    public int addItemID = -1;
    public int removeItemID = -1;

    public int replaceOldID = -1;
    public int replaceNewID = -1;

    [Header("Popup")]
    public GameObject popupUI;
}