using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Created by Jiacheng
 */

public class DifficultySelectionWindow : MonoBehaviour
{
    public Button closeButton;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    public void CloseClicked()
    {
        this.gameObject.SetActive(false);
    }

    public void EasyDifficulty()
    {
        GameData.midiFileName = GameData.songName + CommonParameter.midiDifficulty[0];
        SceneManager.LoadScene("FightingScene");
    }

    public void NormalDifficulty()
    {
        GameData.midiFileName = GameData.songName + CommonParameter.midiDifficulty[1];
        SceneManager.LoadScene("FightingScene");
    }

    public void HardDifficulty()
    {
        GameData.midiFileName = GameData.songName + CommonParameter.midiDifficulty[2];
        SceneManager.LoadScene("FightingScene");
    }
}
