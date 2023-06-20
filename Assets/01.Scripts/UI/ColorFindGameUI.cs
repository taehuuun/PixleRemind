using TMPro;
using UnityEngine;

public class ColorFindGameUI : BodyUI, IObserver
{
    [Header("ColorMatchSystem")] [SerializeField]
    private ColorMatchSystem system;

    [Header("Popups")] public Popup gameOverPopup;

    [Header("Texts")] public TMP_Text scoreText;
    public TMP_Text similarText;
    public TMP_Text lifeText;
    public TMP_Text gameOverPopScoreText;


    private void Awake()
    {
        system.RegisterObserver(this);
    }

    private void Start()
    {
        system.ReStart();
    }

    public void SelectSlot(ColorSlot slot)
    {
        if (system.CheckMatch(slot))
        {
        }
    }

    public void Restart()
    {
        gameOverPopup.gameObject.SetActive(false);
        system.ReStart();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLife(int life)
    {
        lifeText.text = $"기회 : {life.ToString()}";
    }

    public void UpdateSimilarity(float similarity)
    {
        float result = 100 - similarity;
        similarText.text = $"유사도 : {string.Format("{0:N2}", result)} (Max : 95%)";
    }

    // public void UpdateGameOverPop(bool gameOver)
    // {
    //     if (gameOver)
    //     {
    //         OpenPopup(gameOverPopup);
    //         gameOverPopScoreText.text = system.Score.ToString();
    //     }
    //     else
    //     {
    //         gameOverPopup.gameObject.SetActive(false);
    //     }
    // }

    public void UpdateSubjectState()
    {
        UpdateScore(system.Score);
        UpdateLife(system.Life);
        UpdateSimilarity(system.SimilarRange);
        // UpdateGameOverPop(system.IsGameOver);
    }
}