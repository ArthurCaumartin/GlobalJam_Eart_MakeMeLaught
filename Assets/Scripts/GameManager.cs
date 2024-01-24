using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.Dependencies.Sqlite;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private CanvasManager canvasManager;
    [Header("Score Value :")]
    public int totalScore;
    public int perfectScore;
    public int goodScore;
    public int badScore;
    [Space]
    public float kingAffectionChangeValue = 0.05f;
    public float kingAffection = 0.5f;
    public int combo;
    public bool canPlayerSpam = false;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canvasManager.SetAffectionFill(kingAffection);
    }

    public void increaseCombo()
    {
        //0.5,1,2,4,8, prends * 2
    }

    public void AddScore(float distance, Vector3 chordPosition)
    {
        //print("Add Score : distance = " + distance);
        if(distance > .5f)
        {
            totalScore += badScore;
            //print("Bad");
            canvasManager.SetScoreTexte(totalScore, badScore, chordPosition);
            return;
        }

        if(distance < .5f && distance > .2f)
        {
            totalScore += goodScore;
            //print("Good");
            canvasManager.SetScoreTexte(totalScore, goodScore, chordPosition);
            return;
        }

        if(distance < .2f)
        { //! avec 0.07 sa a pas marché ?!
            totalScore += perfectScore;
            //print("Prefect");
            canvasManager.SetScoreTexte(totalScore, perfectScore, chordPosition);
            return;
        }
    }

    public void AddScore(int toAdd, Vector3 chordPosition)
    {
        totalScore += toAdd;
        canvasManager.SetScoreTexte(totalScore, toAdd, chordPosition);
    }

    public void IncreaseKingAffection()
    {
        if(kingAffection >= 1)
            return;
        kingAffection += kingAffectionChangeValue;
        canvasManager.SetAffectionFill(kingAffection);

        if(kingAffection > 1)
        {
            Debug.LogWarning("THE QUEEN WILL BEAT UR ASS UP !!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    public void DeacreaseKingAffection()
    {
        kingAffection -= kingAffectionChangeValue;
        canvasManager.SetAffectionFill(kingAffection);

        if (kingAffection < 0)
        {
            Debug.LogWarning("U ARE DEAD !!");
            OnEndGame();
        }
    }

    private void OnEndGame()
    {
        // DOTween.To((time) =>
        // {
        //     Time.timeScale = time;
        //     print("Time : " + time);
        //     print("Scale : " +  Time.timeScale);
        // }, 1, 0.1f, 5f)
        // .SetUpdate(true);
        Time.timeScale = 0;
        canvasManager.SetEndPanel(totalScore);
    }
}
