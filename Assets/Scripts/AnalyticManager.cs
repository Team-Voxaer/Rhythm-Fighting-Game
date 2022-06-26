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

    /* Update is called once per frame
    void Update()
    {
        
    }*/
    
    public static void OnHitNotes(Direction direction, HitLevel hitLevel, int inputIndex)
	{

        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "Note Hit",
                        new Dictionary<string, object>
                        {
                            { "Hit Level", hitLevel.ToString() },
                            { "Note Number", inputIndex},
                            { "MusicScene", GameData.midiFileName},
                            { "Direction", direction.ToString() }
                        }
                    );//Record Related Data Whenever Hit A Note 1. Hit Level 2. Note Number 3. Generated from which music scene 4. Hit Direction
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnMissNotes(int inputIndex)
    {

        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "Note Miss",
                        new Dictionary<string, object>
                        {
                            { "Note Number", inputIndex},
                            { "MusicScene", GameData.midiFileName},
                        }
                    );//Record Related Data Whenever Miss A Note 1. Note Number 2. Generated from which music scene
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnGameEnd(bool SongEnd)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "Game End",
                        new Dictionary<string, object>
                        {
                            { "Song Ended", SongEnd},
                            { "MusicScene", GameData.midiFileName},
                        }
                    );//Record Related Data Whenever The Game Ended
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    private void OnDestroy()
	{
		Analytics.FlushEvents();
	}
}
