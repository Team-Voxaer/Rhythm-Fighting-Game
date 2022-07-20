using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/**
 * Created by Jiacheng
 */

public class DifficultySelectionWindow : MonoBehaviour
{
    public TextMeshProUGUI messageText;
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
        GameData.difficulty = CommonParameter.midiDifficulty[0].Split('.')[0];
        GameData.midiFileName = GameData.songName + CommonParameter.midiDifficulty[0];
        loadSceneBasedonLevel();
    }

    public void NormalDifficulty()
    {
        GameData.difficulty = CommonParameter.midiDifficulty[1].Split('.')[0];
        GameData.midiFileName = GameData.songName + CommonParameter.midiDifficulty[1];
        loadSceneBasedonLevel();
    }

    public void HardDifficulty()
    {
        GameData.difficulty = CommonParameter.midiDifficulty[2].Split('.')[0];
        GameData.midiFileName = GameData.songName + CommonParameter.midiDifficulty[2];
        loadSceneBasedonLevel();
    }

    private void loadSceneBasedonLevel(){
        if(AnalyticManager.CurrentLevel == 1){
            SceneManager.LoadScene("FishScene");
        }
        else if (AnalyticManager.CurrentLevel == 2){
            SceneManager.LoadScene("FightingScene");
        }
        else if (AnalyticManager.CurrentLevel == 3){
            SceneManager.LoadScene("FishScene");
        }
        else if (AnalyticManager.CurrentLevel == 4){
            SceneManager.LoadScene("FightingScene");
        }
        else if (AnalyticManager.CurrentLevel == 5){
            SceneManager.LoadScene("FishScene");
        }
        else{
            SceneManager.LoadScene("FightingScene");
        }
    }
}
