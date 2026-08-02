using UnityEngine;

[System.Serializable]
public class PuzzleState
{
    public bool autoNext = true;
    public int stateID;

    public int requireItemID = -1;

    public bool consumeItem;

    public int nextState;

    public PuzzleActionData[] actions;
}