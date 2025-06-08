using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSave : MonoBehaviour
{
    public ScoreToJson SavedScore;          // json�ϊ�����f�[�^�̃N���X
    private string filepath;                // json�t�@�C���̃p�X
    private string fileName = "Data.json";  // json�t�@�C����

    void Awake()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;       // �p�X���擾

#elif UNITY_ANDROID
    filepath = Application.persistentDataPath + "/" + fileName;
#elif UNITY_STANDALONE_WIN
    filepath = Application.persistentDataPath + "/" + fileName; 
#endif

        if (!File.Exists(filepath))         // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
        {
            SaveScore();
        }

        SavedScore = Load(filepath);        // �t�@�C����ǂݍ����SavedScore�Ɋi�[
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
        string json = JsonUtility.ToJson(SavedScore);           // json�Ƃ��ĕϊ�
        StreamWriter wr = new StreamWriter(filepath, false);    // �t�@�C���������ݎw��
        wr.WriteLine(json);                                     // json�ϊ�����������������
        wr.Close();                                             // �t�@�C������
    }

    public ScoreToJson Load(string path)
    {
        StreamReader rd = new StreamReader(path);               // �t�@�C���ǂݍ��ݎw��
        string json = rd.ReadToEnd();                           // �t�@�C�����e�S�ēǂݍ���
        rd.Close();                                             // �t�@�C������

        return JsonUtility.FromJson<ScoreToJson>(json);         // json�t�@�C�����^�ɖ߂��ĕԂ�
    }

    public void debug()
    {
        SaveScore();
        SavedScore = Load(filepath);
        var logscore = JsonUtility.ToJson(SavedScore, true);
        Debug.Log(logscore);
    }
}
