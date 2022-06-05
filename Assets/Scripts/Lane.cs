using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lane : MonoBehaviour
{
    // restrict note to certain Key
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    public TextMeshProUGUI scoreText;

    public Bandit bandit;

    // Key for this line
    public KeyCode inputKeyUp;
    public KeyCode inputKeyDown;
    public KeyCode inputKeyLeft;
    public KeyCode inputKeyRight;
    public KeyCode inputKeyCast;

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

            // Current timestamp of audio
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(inputKeyUp)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(0);
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            } else if (Input.GetKeyDown(inputKeyDown)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(1);
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            } else if (Input.GetKeyDown(inputKeyLeft)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(2);
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            } else if (Input.GetKeyDown(inputKeyRight)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(3);
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (Input.GetKeyDown(inputKeyCast)) {
                CastSkill();
            }

            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
                // TODO: to add an animation for missing notes
            }
        }       
    }

    private void Hit(int inputKey)
    {
        print("TODO: Hit");
        hitEffect.ChangeColor();
        scoreManager.Hit(inputKey);
        
    }

    private void Miss()
    {
        print("TODO: Miss");
        scoreManager.Miss();
    }

    private void CastSkill() {
        scoreManager.CastSkill();
    }

}
