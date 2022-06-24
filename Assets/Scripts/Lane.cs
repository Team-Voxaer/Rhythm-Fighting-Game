using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rhythm;

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

    // Combo Display
    public TextMeshProUGUI comboText;

    // Last Hit Level: Perfect / Good / Bad 
    private HitLevel lastHitLevel;

    private int isDefend = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            // Filter through all the notes and find the one with correct noteRestriction
            if (note.NoteName == noteRestriction)
            {
                // Convert Tempo to Metric Time
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
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
            
            if (GameManager.CheckAI() && inputKeyUp == KeyCode.UpArrow) {
                // Use an AI script to operate the right fighter to fight against the left fighter

                Direction direction;
                if (isDefend > 15) {
                    isDefend = 0;
                }
                if (player.GetCurHealth() < player.maxHealth && player.GetCurHealth() >= player.maxHealth * 0.9) {
                    direction = Direction.Down;
                } else if (playerOther.GetCurHealth() <= player.maxHealth * 0.1) {
                    direction = Direction.Up;
                } else if (isDefend < 3) {
                    direction = Direction.Left;
                } else {
                    direction = Direction.Right;
                }
                    
                if (inputIndex % 1 == 0){
                    // HitLevel, Direction = AI.Analyse(audioTime, timeStamp)
                    if (audioTime > timeStamp) {
                        Hit(direction, HitLevel.Perfect);
                        isDefend = isDefend + 1;
                    }
                }
                else{
                    Miss();
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

                    print($"Input Direction {direction.ToString()} {hitLevel.ToString()} on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }

                // If it is larger than Margin of Bad, so it can't even be hit anymore
                if (timeStamp + marginOfBad <= audioTime)
                {
                    Miss();
                    print($"Missed {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    // TODO: to add an animation for missing notes
                }

                ShowHitlevel();
            }
        }       
    }

    private void Hit(Direction direction, HitLevel hitLevel)
    {
        // print($"Hit on {inputIndex} note");
        Destroy(notes[inputIndex].gameObject);

        lastHitLevel = hitLevel;

        hitEffect.ChangeColor((int) direction);
        scoreManager.Hit(direction, hitLevel, inputIndex); 

        inputIndex++;
    }

    private void Miss()
    {
        scoreManager.Miss(inputIndex);
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
        string text = "Hit Level: " + lastHitLevel.ToString();
        comboText.text = text;
    }

}
