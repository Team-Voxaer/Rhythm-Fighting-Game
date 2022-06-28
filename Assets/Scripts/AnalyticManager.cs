using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Rhythm;

public class AnalyticManager : MonoBehaviour
{
    public static int CurrentLevel;
    public static int inputIndexDistributionBit;
    // 0 - Perfect / 1 - Good / 2 - Bad / 3 - Miss
    private static List<int> HitLevelCounts = new List<int>(){0, 0, 0, 0}; 
    private static List<int> ComboCounts = new List<int>(){0, 0, 0, 0, 0, 0};
    private static List<int> InputIndexDistribution = new List<int>() { 0, 0, 0, 0, 0, 0 };
    private static List<int> DefenseCounts = new List<int>(){0, 0};

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    
    public static void OnHitNotes(Direction direction, HitLevel hitLevel, int inputIndex)
	{
        HitLevelCounts[(int) hitLevel] += 1;  // 0 - Perfect; 1 - Good; 2 - Bad; 3 - Miss
    }
	
    public static void OnMissNotes(int inputIndex)
    {
        HitLevelCounts[3] += 1; // 0 - Perfect; 1 - Good; 2 - Bad; 3 - Miss
    }

    public static void OnFirstCombo(int comboCode, int inputIndex)
	{
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "firstCombo",
                        new Dictionary<string, object>
                        {   { "comboType", comboCode.ToString()},
                            { "noteIndex", inputIndex},
                            { "musicScene", GameData.midiFileName}
                        }
            );//Record Related Data Whenever Player Hit first combo
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded

        //Debug.Log("OnFirstCombo " + comboCode.ToString());
        /*
        Zhian Li: comboCode list
        protected const int skillCodeDefend = 0;
        protected const int skillCodeAttack = 1;
        protected const int skillCodeSword = 2;
        protected const int skillCodeGrandCross = 3;
        protected const int skillCodeThunder = 4;
        protected const int skillCodeHealing = 5;
        */

    }

    public static void OnSuccessDefense(bool defenseTrigger)
    {
        if (defenseTrigger) DefenseCounts[1] += 1;
        else DefenseCounts[0] += 1;

    }

    public static void OnComboReleased(int comboCode, int noteIndex)
    {
        ComboCounts[comboCode] += 1;
        Debug.Log("OnComboReleased" + noteIndex.ToString());
        
        if (noteIndex <= 5)
        {
            inputIndexDistributionBit = 0;
            InputIndexDistribution[inputIndexDistributionBit] += 1;
        }
        else if ((noteIndex > 5) && (noteIndex <= 10))
        {
            inputIndexDistributionBit = 1;
            InputIndexDistribution[inputIndexDistributionBit] += 1;
        }
        else if ((noteIndex > 10) && (noteIndex <= 20))
        {
            inputIndexDistributionBit = 2;
            InputIndexDistribution[inputIndexDistributionBit] += 1;
        }
        else if ((noteIndex > 20) && (noteIndex <= 30))
        {
            inputIndexDistributionBit = 3;
            InputIndexDistribution[inputIndexDistributionBit] += 1;
        }
        else if ((noteIndex > 30) && (noteIndex <= 40))
        {
            inputIndexDistributionBit = 4;
            InputIndexDistribution[inputIndexDistributionBit] += 1;
        }
        else if (noteIndex > 41)
        {
            inputIndexDistributionBit = 5;
            InputIndexDistribution[inputIndexDistributionBit] += 1;
        }

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

    public static void OnLevelEnd(string reason)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "levelEnd",
                        new Dictionary<string, object>
                        {
                            { "endReason", reason},
                            { "musicScene", GameData.midiFileName}
                        }
                    );//Record Related Data Whenever The Game Ended
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded

        UploadHitLevelCounts();
        UploadComboCounts();
        UploadDefenseCounts();
        UploadComboHitIndexDistribution();

        ClearStats();
    }

    private static void UploadHitLevelCounts(){
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "hitLevelCounts",
                        new Dictionary<string, object>
                        {
                            { "perfect", HitLevelCounts[0]},
                            { "good", HitLevelCounts[1]},
                            { "bad", HitLevelCounts[2]},
                            { "miss", HitLevelCounts[3]},
                            { "musicScene", GameData.midiFileName}
                        }
                    );
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
        /*
        Debug.Log("HitLevelCounts " + HitLevelCounts[0].ToString() + " " + HitLevelCounts[1].ToString() 
         + " " + HitLevelCounts[2].ToString()  + " " + HitLevelCounts[3].ToString()); // Confirm
         */
    }

    private static void UploadComboCounts(){
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "comboCounts",
                        new Dictionary<string, object>
                        {
                            { "defend", ComboCounts[0]},
                            { "attack", ComboCounts[1]},
                            { "sword", ComboCounts[2]},
                            { "grandCross", ComboCounts[3]},
                            { "thunder", ComboCounts[4]},
                            { "healing", ComboCounts[5]},
                            { "musicScene", GameData.midiFileName}
                        }
                    );
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
        
        /*
        Debug.Log("ComboCounts " + ComboCounts[2].ToString() + " " + ComboCounts[3].ToString() 
         + " " + ComboCounts[4].ToString()  + " " + ComboCounts[5].ToString()); // Confirm
        */
         
    }
    
    private static void UploadComboHitIndexDistribution()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "comboHitIndexDistri",
                        new Dictionary<string, object>
                        {
                            { "index5", InputIndexDistribution[0]},
                            { "index10", InputIndexDistribution[1]},
                            { "index20", InputIndexDistribution[2]},
                            { "index30", InputIndexDistribution[3]},
                            { "index40", InputIndexDistribution[4]},
                            { "indexMorethan40", InputIndexDistribution[5]},
                            { "musicScene", GameData.midiFileName}
                        }
                    );
        Debug.Log("UploadComboHitIndexDistributionanalyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded

        
        

    }

    private static void UploadDefenseCounts(){
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
                        "defenseCounts",
                        new Dictionary<string, object>
                        {
                            { "defenseFail", DefenseCounts[0]},
                            { "defenseSuccess", DefenseCounts[1]},
                            { "musicScene", GameData.midiFileName},
                        }
                    );
        Debug.Log("analyticsResult: " + analyticsResult);// Make sure analytics log has been uploaded
        
        /*
        Debug.Log("DefenseCounts " + DefenseCounts[0].ToString() + " " + DefenseCounts[1].ToString()); // Confirm
        */
         
    }

    private static void ClearStats()
    {
        for(int i = 0; i < HitLevelCounts.Count; i++)
        {
            HitLevelCounts[i] = 0;
        }

        for(int i = 0; i < ComboCounts.Count; i++)
        {
            ComboCounts[i] = 0;
        }

        for(int i = 0; i < DefenseCounts.Count; i++){
            DefenseCounts[i] = 0;
        }
    }

    private void OnDestroy()
	{
		Analytics.FlushEvents();
	}
}
