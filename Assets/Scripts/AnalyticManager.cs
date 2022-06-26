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
                            { "HitLevel", hitLevel.ToString() },
                            { "NoteIndex", inputIndex},
                            { "MusicScene", GameData.midiFileName},
                            { "Direction", direction.ToString() }
                        }
                    );//Record Related Data Whenever Hit A Note 1. Hit Level 2. Note Number 3. Generated from which music scene 4. Hit Direction
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }
    
     public static void OnLevelSelected(Direction direction, HitLevel hitLevel, int inputIndex)
	{

	AnalyticsResult analyticsResult = Analytics.levelSelected(
			"noteHit",
			new Dictionary<string, object>
			{
			    { "HitLevel", hitLevel.ToString() },
			    { "NoteIndex", inputIndex},
			    { "MusicScene", GameData.midiFileName},
			    { "Direction", direction.ToString() }
			}
		    );//Record Related Data Whenever Hit A Note 1. Hit Level 2. Note Number 3. Generated from which music scene 4. Hit Direction
	Debug.Log("analyticsResult: " + analyticsResult);// Select the game level and keep the level in memory
        }
	
      public static void OnComboReleased(Direction direction, HitLevel hitLevel, int inputIndex)
      {

          AnalyticsResult analyticsResult = Analytics.levelSelected(
			"noteHit",
			new Dictionary<string, object>
			{
			    { "HitLevel", hitLevel.ToString() },
			    { "NoteIndex", inputIndex},
			    { "MusicScene", GameData.midiFileName},
			    { "Direction", direction.ToString() }
			}
		    );//Record Related Data Whenever Hit A Note 1. Hit Level 2. Note Number 3. Generated from which music scene 4. Hit Direction
	Debug.Log("analyticsResult: " + analyticsResult);// Release one combo along with the music file
       }
	
    public static void OnMissNotes(int inputIndex)
    {

        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "noteMiss",
                        new Dictionary<string, object>
                        {
                            { "NoteIndex", inputIndex},
                            { "MusicScene", GameData.midiFileName},
                        }
                    );//Record Related Data Whenever Miss A Note 1. Note Number 2. Generated from which music scene
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
    }

    public static void OnLevelEnd(bool SongEnd)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "levelEnd",
                        new Dictionary<string, object>
                        {
                            { "SongEnded", SongEnd},
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
