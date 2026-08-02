using UnityEngine;

public class Map2Manager : MonoBehaviour
{
    public static Map2Manager Instance;


    // Quạ
    public bool crowPlaced = false;
    public bool crowAlive = false;
    public bool crowFed = false;


    // Lông quạ
    public bool featherTaken = false;


    // Tranh bồ câu
    public bool pigeonSolved = false;


    // Quả cầu xanh
    public bool orbPlaced = false;


    // Tượng khóc
    public bool statueCry = false;


    // Lọ nước mắt
    public bool tearsTaken = false;


    // Vẽ tranh
    public bool paintingDone = false;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}