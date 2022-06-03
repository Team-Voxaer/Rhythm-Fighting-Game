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
    public KeyCode inputKey;
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

    int hp = 100;

    // List of keys for skills
    List<int> lsKey = new List<int>();
    List<int> lsSkill = new List<int>();

    public ScoreManager scoreManager;
    // public SkillManager skillManager;

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
        // For Display
        scoreText.text = hp.ToString();

        // For Game Maintenance

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
                    Hit(inputKeyUp);
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
                    Hit(inputKeyDown);
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
                    Hit(inputKeyLeft);
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
                    Hit(inputKeyRight);
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

    private void Hit(KeyCode inputKey)
    {
        print("TODO: Hit");
        // scoreManager.Hit();
        // SkillManager.Hit(inputKey);
        if (inputKey == inputKeyUp) {
            lsKey.Add(0);
        } else if (inputKey == inputKeyDown) {
            lsKey.Add(1);
        } else if (inputKey == inputKeyLeft) {
            lsKey.Add(2);
        } else if (inputKey == inputKeyRight) {
            lsKey.Add(3);
        }
    }

    private void Miss()
    {
        print("TODO: Miss");
        scoreManager.Miss();
    }

    private void CastSkill() {
        CheckSkills();
        if (lsSkill.Count > 0) {
            bandit.Attack();
            lsSkill.RemoveAt(0);
        }
    }

    private void CheckSkills() {
        while (lsKey.Count > 3) {
            lsKey.RemoveAt(0);
        }
        if (lsKey[-1] == 3 && lsKey[-2] == 1 && lsKey[-1] == 1)  // Down Down Right
        {
            lsSkill.Add(0);
            for (int i=0; i<3; i++) {
                lsKey.RemoveAt(-1);
            }
        }
    }

}
