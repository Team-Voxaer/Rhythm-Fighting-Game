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
		Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("Direction", direction.ToString());
		data.Add("KilledBy", hitLevel.ToString());
        data.Add("Midi", GameData.midiFileName);
        data.Add("Level", CurrentLevel);

		Analytics.CustomEvent("HitNotes", data);

        Debug.Log("AnalyticManager::OnHitNotes");
	}

    private void OnDestroy()
	{
		Analytics.FlushEvents();
	}
}
