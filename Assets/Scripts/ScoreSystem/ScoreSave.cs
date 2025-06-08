using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSave : MonoBehaviour
{
    public ScoreToJson SavedScore;          // json変換するデータのクラス
    private string filepath;                // jsonファイルのパス
    private string fileName = "Data.json";  // jsonファイル名

    void Awake()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;       // パス名取得

#elif UNITY_ANDROID
    filepath = Application.persistentDataPath + "/" + fileName;
#elif UNITY_STANDALONE_WIN
    filepath = Application.persistentDataPath + "/" + fileName; 
#endif

        if (!File.Exists(filepath))         // ファイルがないとき、ファイル作成
        {
            SaveScore();
        }

        SavedScore = Load(filepath);        // ファイルを読み込んでSavedScoreに格納
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScore()
    {
        string json = JsonUtility.ToJson(SavedScore);           // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
    }

    public ScoreToJson Load(string path)
    {
        StreamReader rd = new StreamReader(path);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる

        return JsonUtility.FromJson<ScoreToJson>(json);         // jsonファイルを型に戻して返す
    }

    public void debug()
    {
        SaveScore();
        SavedScore = Load(filepath);
        var logscore = JsonUtility.ToJson(SavedScore, true);
        Debug.Log(logscore);
    }
}
