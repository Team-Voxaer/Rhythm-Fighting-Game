using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rhythm;
using UnityEngine.Analytics;

namespace Rhythm
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public enum HitLevel
    {
        Perfect,
        Good,
        Bad,
        Invalid
    }
}

struct AI {
    public Direction curDirect;
    public Direction preDirect;
    public int isCollectGrandCross;
    public int totalCount;
}



public class Lane : MonoBehaviour
{
    

    // restrict note to certain Key
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    public PlayerController player;
    public PlayerController playerOther;

    // Key for this line
    public KeyCode inputKeyUp;
    public KeyCode inputKeyDown;
    public KeyCode inputKeyLeft;
    public KeyCode inputKeyRight;
    
    // Template for Note
    public GameObject notePrefab;

    // List of Notes in this line
    List<Note> notes = new List<Note>();

    // List of time the player need to press the input
    public List<double> timeStamps = new List<double>();

    // Keep track of what timestamp needs to be spawned
    int spawnIndex = 0;

    // Keep track of what timestamp needs to be detacted 
    int inputIndex = 0;

    public ScoreManager scoreManager;

    // Hitting Effect
    public HitEffect hitEffect;
    public GameObject hitLevelObj;
    private Sprite goodLevel, badLevel;

    /*//Score Effect
    public GameObject floatingPoints;
    public GameObject perfect;
    public GameObject good;
    public GameObject bad;*/


    // Combo Display
    public TextMeshProUGUI comboText;

    // Last Hit Level: Perfect / Good / Bad 
    private HitLevel lastHitLevel;

    private AI ai = new AI{isCollectGrandCross = 0, totalCount = 0};

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Object[] sprites = Resources.LoadAll("HitLevelImage/rankImage2");
        goodLevel = (Sprite)sprites[3];
        badLevel = (Sprite)sprites[4];
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array, float songDelayInSeconds)
    {
        foreach (var note in array)
        {
            // Filter through all the notes and find the one with correct noteRestriction
            if (note.NoteName == noteRestriction)
            {
                // Convert Tempo to Metric Time
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                /**
                 * Jiacheng
                 * If running on WebGL, we need to add "songDelayInSeconds" to match nodes with song delay time, 
                 * and also an "offset" to counteract the WebGL delay;
                 * If running locally, we cannot add "songDelayInSeconds" as well as "offset", otherwise would cause huge delay of notes.
                 */
                if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://")) {
                    double offset = -.1f;
                    timeStamps.Add((double)songDelayInSeconds + offset + 
                    (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                } else {
                    timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            // Timestamp of current note should be pressed in
            double timeStamp = timeStamps[inputIndex];
            
            // Range: Perfect / Good / Bad / Invalid 
            double marginOfGood = SongManager.Instance.marginOfError;
            double marginOfPerfect = marginOfGood / 4;
            double marginOfBad = marginOfGood + marginOfPerfect; 

            // Current timestamp of audio
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);
            
            if (GameData.AILevel != 0 && inputKeyUp == KeyCode.UpArrow) {
                // Use an AI script to operate the right fighter to fight against the left fighter
                if (GameData.AILevel == 1) {
                    AIEasy(audioTime, timeStamp, marginOfBad);
                } else if (GameData.AILevel == 2) {
                    AIMedium(audioTime, timeStamp, marginOfBad);
                } else if (GameData.AILevel == 3) {
                    AIHard(audioTime, timeStamp, marginOfBad);
                } 
            } else {

                // Check Direction Key Input
                Direction direction;
                if (Input.GetKeyDown(inputKeyUp)) {
                    direction = Direction.Up;
                } else if (Input.GetKeyDown(inputKeyDown)) {
                    direction = Direction.Down;
                } else if (Input.GetKeyDown(inputKeyLeft)) {
                    direction = Direction.Left;
                } else if (Input.GetKeyDown(inputKeyRight)) {
                    direction = Direction.Right;
                } else {
                    direction = Direction.None;
                }
                
                // Which means someone actually hit a direction key
                if (direction != Direction.None) { 
                    HitLevel hitLevel;
                    if (Math.Abs(audioTime - timeStamp) < marginOfPerfect) {
                        hitLevel = HitLevel.Perfect;
                    }
                    else if (Math.Abs(audioTime - timeStamp) < marginOfGood)
                    {
                        hitLevel = HitLevel.Good;
                    }
                    else if (Math.Abs(audioTime - timeStamp) < marginOfBad)
                    {
                        hitLevel = HitLevel.Bad;
                    }
                    else {
                        hitLevel = HitLevel.Invalid;
                    }

                    if (hitLevel != HitLevel.Invalid) {
                        Hit(direction, hitLevel);
                    }

                    // print($"Input Direction {direction.ToString()} {hitLevel.ToString()} on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");


                }

                // If it is larger than Margin of Bad, so it can't even be hit anymore
                if (timeStamp + marginOfBad <= audioTime)
                {
                    Miss();
                    // print($"Missed {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    // TODO: to add an animation for missing notes
                }

                ShowHitlevel();
            }
        }       
    }

    /*//My Score Function
    void showScore(HitLevel hitLevel){
         if(hitLevel == Rhythm.HitLevel.Perfect){
            GameObject perfectText = Instantiate(perfect, gameObject.transform.position, Quaternion.identity);   
        }
        else if(hitLevel == Rhythm.HitLevel.Good){
            GameObject perfectText = Instantiate(good, gameObject.transform.position, Quaternion.identity); 
        }
        else if(hitLevel == Rhythm.HitLevel.Bad){
            GameObject perfectText = Instantiate(bad, gameObject.transform.position, Quaternion.identity); 
        }
        else{
            print("Miss");
        }
    }*/

    private void Hit(Direction direction, HitLevel hitLevel)
    {
        // print($"Hit on {inputIndex} note");
        Destroy(notes[inputIndex].gameObject);

        lastHitLevel = hitLevel;

        GameObject hitLevelInstance =  Instantiate(hitLevelObj, transform.position, Quaternion.identity);

        if (lastHitLevel == HitLevel.Good)
        {
            hitLevelInstance.GetComponent<SpriteRenderer>().sprite = goodLevel;
        }
        else if (lastHitLevel == HitLevel.Bad)
        {
            hitLevelInstance.GetComponent<SpriteRenderer>().sprite = badLevel;
        }


        hitEffect.ChangeColor((int) direction);
        /*//Show Score Sprite
        showScore(hitLevel);*/

        scoreManager.Hit(direction, hitLevel, inputIndex); 
        if (!(GameManager.CheckAI() && inputKeyUp == KeyCode.UpArrow)){ // If this is not an AI Hit
            AnalyticManager.OnHitNotes(direction, hitLevel, inputIndex); // Send AnalyticsManager notehit data
        }
        inputIndex++;
    }

    private void Miss()
    {
        scoreManager.Miss(inputIndex);
        if (!(GameManager.CheckAI() && inputKeyUp == KeyCode.UpArrow)){ // If this is not an AI Miss
            AnalyticManager.OnMissNotes(inputIndex); //Send AnalyticsManager notemiss data
        }
        inputIndex++;
        lastHitLevel = HitLevel.Invalid;
    }

    /*void AddCombo(string combo)
    {
        if (comboQueue.Count == 3)
        {
            comboQueue.Dequeue();
        }
        comboQueue.Enqueue(combo);
    }
    */

    void ShowHitlevel()
    {
        // string text = "Hit Level: " + lastHitLevel.ToString();
        // comboText.text = text;
    }


    void AIEasy(double audioTime, double timeStamp, double marginOfBad) {
        if (player.GetIsUseGrandCross() == false && ai.isCollectGrandCross > 3) {
            ai.isCollectGrandCross = 0;
        }

        // // If the AI is initiated before the game begains, uncomment the following block.
        if (ai.totalCount == 0) {
            ai.curDirect = Direction.Right;
        } else if (ai.isCollectGrandCross < 2) {
            ai.curDirect = Direction.Left;
        } else if (playerOther.GetIsUseGrandCross()) {
            ai.curDirect = Direction.Up;
        } else {
            ai.curDirect = Direction.Right;
        } 
        // The logic to call healing. For now, we do not use it.
        // if (player.GetCurHealth() < player.maxHealth && player.GetCurHealth() >= player.maxHealth * 0.9) {
        //     ai.curDirect = Direction.Down;
        // }

        // If this time step is in the process of collecting another skill, still to the previous skill.
        if (ai.totalCount % 3 == 0) {
            // The previous skill has been collected and casted. Start to collect new skills.
            ai.preDirect = ai.curDirect;
        } else {
            // The previous skill is still in the collection process. Stick to the previous skill.
            ai.curDirect = ai.preDirect;
        }

        if (Math.Abs(audioTime - timeStamp) < marginOfBad) {
            double rndDouble = UnityEngine.Random.value;
            if (rndDouble > 0.9) {
                Hit(ai.curDirect, HitLevel.Perfect);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else if (rndDouble > 0.6) {
                Hit(ai.curDirect, HitLevel.Good);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else if (rndDouble > 0.5) {
                Hit(ai.curDirect, HitLevel.Bad);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else {
                Miss();
            }
        }
    }


    void AIMedium(double audioTime, double timeStamp, double marginOfBad) {
        if (player.GetIsUseGrandCross() == false && ai.isCollectGrandCross > 3) {
            ai.isCollectGrandCross = 0;
        }

        // // If the AI is initiated before the game begains, uncomment the following block.
        if (ai.totalCount == 0) {
            ai.curDirect = Direction.Right;
        } else if (ai.isCollectGrandCross < 2) {
            ai.curDirect = Direction.Left;
        } else if (playerOther.GetIsUseGrandCross()) {
            ai.curDirect = Direction.Up;
        } else {
            ai.curDirect = Direction.Right;
        } 
        // The logic to call healing. For now, we do not use it.
        // if (player.GetCurHealth() < player.maxHealth && player.GetCurHealth() >= player.maxHealth * 0.9) {
        //     ai.curDirect = Direction.Down;
        // }

        // If this time step is in the process of collecting another skill, still to the previous skill.
        if (ai.totalCount % 3 == 0) {
            // The previous skill has been collected and casted. Start to collect new skills.
            ai.preDirect = ai.curDirect;
        } else {
            // The previous skill is still in the collection process. Stick to the previous skill.
            ai.curDirect = ai.preDirect;
        }

        if (Math.Abs(audioTime - timeStamp) < marginOfBad) {
            double rndDouble = UnityEngine.Random.value;
            if (rndDouble > 0.7) {
                Hit(ai.curDirect, HitLevel.Perfect);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else if (rndDouble > 0.4) {
                Hit(ai.curDirect, HitLevel.Good);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else if (rndDouble > 0.3) {
                Hit(ai.curDirect, HitLevel.Bad);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else {
                Miss();
            }
        }
    }


    void AIHard(double audioTime, double timeStamp, double marginOfBad) {
        if (player.GetIsUseGrandCross() == false && ai.isCollectGrandCross > 2) {
            ai.isCollectGrandCross = 0;
        }

        // // If the AI is initiated before the game begains, uncomment the following block.
        if (ai.totalCount == 0) {
            ai.curDirect = Direction.Right;
        } else if (ai.isCollectGrandCross < 2) {
            ai.curDirect = Direction.Left;
        } else if (playerOther.GetIsUseGrandCross()) {
            ai.curDirect = Direction.Up;
        } else {
            ai.curDirect = Direction.Right;
        } 
        // The logic to call healing. For now, we do not use it.
        // if (player.GetCurHealth() < player.maxHealth && player.GetCurHealth() >= player.maxHealth * 0.9) {
        //     ai.curDirect = Direction.Down;
        // }

        // If this time step is in the process of collecting another skill, still to the previous skill.
        if (ai.totalCount % 3 == 0) {
            // The previous skill has been collected and casted. Start to collect new skills.
            ai.preDirect = ai.curDirect;
        } else {
            // The previous skill is still in the collection process. Stick to the previous skill.
            ai.curDirect = ai.preDirect;
        }
        
        // if (audioTime > timeStamp) {
        if (Math.Abs(audioTime - timeStamp) < marginOfBad) {
            if (inputIndex % 1 == 0){
                // HitLevel, Direction = AI.Analyse(audioTime, timeStamp)
                Hit(ai.curDirect, HitLevel.Perfect);
                ai.isCollectGrandCross ++;
                ai.totalCount ++;
            } else {
                Miss();
            }
        }
    }

}
