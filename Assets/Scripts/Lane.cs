using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Lane : MonoBehaviour
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // restrict note to certain Key
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    public PlayerController player;

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

    /*// Combo Queue
    private Queue<string> comboQueue = new Queue<string>();*/

    // Combo Display
    public TextMeshProUGUI comboText;

    // Last Hit Level: Perfect / Good
    private string lastHitLevel;

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
            double marginOfError = SongManager.Instance.marginOfError;
            double marginOfPerfect = marginOfError / 2;
            // Current timestamp of audio
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(inputKeyUp)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfPerfect) {
                    Hit(Direction.UP, true);
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(Direction.UP, false);
                }
                else 
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            } else if (Input.GetKeyDown(inputKeyDown)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfPerfect) {
                    Hit(Direction.DOWN, true);
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(Direction.DOWN, false);
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            } else if (Input.GetKeyDown(inputKeyLeft)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfPerfect) {
                    Hit(Direction.LEFT, true);
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(Direction.LEFT, false);
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            } else if (Input.GetKeyDown(inputKeyRight)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfPerfect) {
                    Hit(Direction.RIGHT, true);
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(Direction.RIGHT, false);
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            

            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
                // TODO: to add an animation for missing notes
            }

            //ShowCombo();
        }       
    }

    private void Hit(Direction direction, bool isPerfect)
    {
        print($"Hit on {inputIndex} note");
        Destroy(notes[inputIndex].gameObject);
        inputIndex++;

        if (isPerfect) lastHitLevel = "Perfect";
        else lastHitLevel = "Good";

        switch (direction)
        {
        case Direction.UP:
            //AddCombo("UP");
            break;
        case Direction.DOWN:
            //AddCombo("DOWN");
            break;
        case Direction.LEFT:
            //AddCombo("LEFT");
            break;
        case Direction.RIGHT:
            //AddCombo("RIGHT");
            break;
        }

        hitEffect.ChangeColor((int) direction);
        scoreManager.Hit((int) direction); 
    }

    private void Miss()
    {
        scoreManager.Miss();
    }

    /*void AddCombo(string combo)
    {
        if (comboQueue.Count == 3)
        {
            comboQueue.Dequeue();
        }
        comboQueue.Enqueue(combo);
    }

    void ShowCombo()
    {
        string text = "Input History:" + String.Join(" ", comboQueue) + "\nHit Level: " + lastHitLevel;
        comboText.text = text;
    }*/

}
