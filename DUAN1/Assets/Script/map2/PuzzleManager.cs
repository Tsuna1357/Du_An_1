using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;



    // QUẠ


    public bool crowPlaced = false;
    public bool crowAlive = false;
    public bool crowFed = false;




    // LÔNG QUẠ


    public bool featherTaken = false;




    // TRANH BỒ CÂU


    public bool pigeonSolved = false;




    // QUẢ CẦU XANH


    public bool orbTaken = false;
    public bool orbPlaced = false;




    // TƯỢNG


    public bool statueCry = false;




    // LỌ NƯỚC MẮT


    public bool bottleFilled = false;




    // VẼ TRANH


    public bool brushDipped = false;
    public bool paintingDone = false;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}