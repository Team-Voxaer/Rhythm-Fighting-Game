using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    // restrict note to certain Key
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    // Key for this line
    public KeyCode input;

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

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
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
            }
        }       
    }

    private void Hit()
    {
        print("TODO: Hit");
        //ScoreManager.Hit();
    }
    private void Miss()
    {
        print("TODO: Miss");
        //ScoreManager.Miss();
    }
    
}
