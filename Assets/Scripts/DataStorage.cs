using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    [SerializeField] TMP_InputField nameField;

    int highScore = 0;
    string playerName;
    string highscorePlayerName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    

    public void SetPlayerName()
    {
        if(nameField.text == null)
        {
            return;
        }

        this.playerName = nameField.text;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetHighScore(int score)
    {
        highScore = score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void SetHighScorePlayerName(string hsPlayerName)
    {
        highscorePlayerName = hsPlayerName;
    }

    public string GetHighScorePlayerName()
    {
        return highscorePlayerName;
    }

    public void StartGame()
    {
        SetPlayerName();
        SceneManager.LoadScene("main");
    }

    public void Exit()
    {
        SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScorePlayerName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScorePlayerName = highscorePlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highscorePlayerName = data.highScorePlayerName;
        }
    }
}
