using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Rhythm;

public class AnalyticManager : MonoBehaviour
{
    public static int CurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    
    public static void OnHitNotes(Direction direction, HitLevel hitLevel, int inputIndex)
	{

        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "noteHit",
                        new Dictionary<string, object>
                        {
                            { "hitLevel", hitLevel.ToString() },
                            { "noteIndex", inputIndex},
                            { "musicScene", GameData.midiFileName},
                            { "direction", direction.ToString() }
                        }
                    );//Record Related Data Whenever Hit A Note 1. Hit Level 2. Note Number 3. Generated from which music scene 4. Hit Direction
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }
	
    public static void OnMissNotes(int inputIndex)
    {

        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "noteMiss",
                        new Dictionary<string, object>
                        {
                            { "noteIndex", inputIndex},
                            { "musicScene", GameData.midiFileName},
                        }
                    );//Record Related Data Whenever Miss A Note 1. Note Number 2. Generated from which music scene
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnSuccessDefense(bool defenseTrigger)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "successDefense",
                        new Dictionary<string, object>
                        {
                            { "successDefense", defenseTrigger},
                            { "musicScene", GameData.midiFileName},
                        }
                    );//Record Related Data Whenever The Game Ended
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnComboReleased(string playerCombo)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "comboRelease",
                        new Dictionary<string, object>
                        {
                            { "comboType", playerCombo},
                            { "musicScene", GameData.midiFileName}
                        }
                    );//Record Related Data Whenever The Game Ended
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnLevelSelected(Direction direction, HitLevel hitLevel, int inputIndex)
    {

        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "levelSelected",
                        new Dictionary<string, object>
                        {
                            { "musicScene", GameData.midiFileName}
                        }
            );//Record Related Data Whenever Select A Scene：Select the game level and keep the level in memory
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnLevelEnd(bool songEnd)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "levelEnd",
                        new Dictionary<string, object>
                        {
                            { "songEnded", songEnd},
                            { "musicScene", GameData.midiFileName},
                        }
                    );//Record Related Data Whenever The Game Ended
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    private void OnDestroy()
	{
		Analytics.FlushEvents();
	}
}
