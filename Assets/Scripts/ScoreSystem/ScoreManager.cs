using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public ScoreSave scoreSave;
    private ScoreToJson SavedScore;
    private int YourScore;
    private int[] Scores;
    private int NumScore;
    private int HighScore;

    private int MidScore;
    private int AveScore;
    private int TopOfScores;

    public TMP_Text YourText;
    public TMP_Text HighText;
    public TMP_Text AveText;
    public TMP_Text MidText;
    public TMP_Text TopText;

    public GameObject HighUpdate;

    public static bool ClearGame = false;

    // Start is called before the first frame update
    void Start()
    {
        if (ClearGame == false)
        {
            SavedScore = scoreSave.SavedScore;
            LoadScore();
            SetScoreText();
        }
        else
        {
            SavedScore = scoreSave.SavedScore;
            LoadScore();
            EndAndSave();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadScore()
    {
        Scores = SavedScore.Scores;
        NumScore = Scores.Length;
        HighScore = SavedScore.HighScore;
        MidScore = SavedScore.MidScore;
        AveScore = SavedScore.AveScore;
    }

    private void SetScoreText()
    {
        HighText.SetText(HighScore.ToString());
        AveText.SetText(AveScore.ToString());
        MidText.SetText(MidScore.ToString());
    }






    public void EndAndSave()
    {
        YourScore = GameManager.totalscore;
        AddYourScore();
        SortScores();

        TopSearch();
        CompareHighScore();

        GetMidScore();
        GetAverage();

        SaveScore();
        SetEndScoreText();
    }

    private void AddYourScore()
    {
        int[] newScores = new int[NumScore + 1];

        for (int i = 0; i < NumScore; i++)
        {
            newScores[i] = Scores[i];
        }

        newScores[NumScore] = YourScore;

        Scores = newScores;

        NumScore = Scores.Length;
    }

    private void SortScores()
    {
        Array.Sort(Scores);        // スコアを小さい順にソート
    }

    private void TopSearch()
    {
        int index = System.Array.BinarySearch(Scores, YourScore);
        if (index < 0)
        {
            index = ~index; // ビット反転で正しい挿入位置を得る
        }

        TopOfScores = (int)((float)index / NumScore * 100);
    }

    private void CompareHighScore()
    {
        if (HighScore < YourScore)
        {
            HighScore = YourScore;

            HighUpdate.SetActive(true);
            HighText.color = Color.yellow;
        }
    }

    private void GetMidScore()
    {
        if (NumScore % 2 == 1)
        {
            MidScore = Scores[NumScore / 2];  // 真ん中の値
        }
        else
        {
            // 配列の要素数が偶数の場合、真ん中の2つの値の平均を計算
            int middleIndex1 = (NumScore / 2) - 1;
            int middleIndex2 = NumScore / 2;
            MidScore = (Scores[middleIndex1] + Scores[middleIndex2]) / 2;
        }
    }

    private void GetAverage()
    {
        int sum = 0;
        for (int i = 0; i < NumScore; i++)
        {
            sum += Scores[i];  // 全ての要素を足す
        }
        AveScore = sum / NumScore;  // 合計を要素数で割る
    }

    private void SaveScore()
    {
        scoreSave.SavedScore.Scores = Scores;
        scoreSave.SavedScore.HighScore = HighScore;
        scoreSave.SavedScore.MidScore = MidScore;
        scoreSave.SavedScore.AveScore = AveScore;
        scoreSave.SaveScore();
    }
    private void SetEndScoreText()
    {
        YourText.SetText(YourScore.ToString());
        HighText.SetText(HighScore.ToString());
        AveText.SetText(AveScore.ToString());
        MidText.SetText(MidScore.ToString());
        TopText.SetText(TopOfScores.ToString());
    }
}
