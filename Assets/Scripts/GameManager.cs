using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public TMP_InputField NameInput;
    public string PlayerName;
    public int actualHighScore =0;
    public string HighPlayerName="-";
    public TMP_Text HighNameText;
    public TMP_Text HighScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
        HighNameText.text = "Name: " + HighPlayerName;
        HighScoreText.text = "Score: " + actualHighScore;
    }
   
    public void SetPlayerName()
    {
        PlayerName = NameInput.text;
        //Debug.Log(PlayerName);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
       

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string HighScoreName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScore = actualHighScore;
        data.HighScoreName = HighPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            actualHighScore = data.HighScore;
            HighPlayerName = data.HighScoreName;
        }
    }
}


