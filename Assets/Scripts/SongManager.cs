using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;  // an instance of current class SongManager
    public static MidiFile midiFile;  // midi file
    public AudioSource audioSource;  // to play audio, imported from UnityEngine
    public Lane[] lanes;  // how many lanes of note
    public float songDelayInSeconds;  // delay playing song after a certain amount of time
    public double marginOfError;  // user mis-tap error line, in seconds
    public int inputDelayInMilliseconds;  // keyboard delay time in case of input delay
    public string fileLocation;  // file location of midi file
    public float noteTime;  // how much time the note will be on the screen, from noteSpawnY to noteTapY
    public float noteSpawnY;  // note spawn position
    public float noteTapY;  // note need-to-tap position
    public float noteDespawnY {  // note despawn position
        get {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    // get audio source current playback time
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());  // read file from webgl resources
        }
        else
        {
            ReadFromFile();  // read file from local
        }
    }

    // Update is called once per frame
    void Update() { }

    // Read midi file from website
    private IEnumerator ReadFromWebsite() {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                Debug.LogError(www.error);
            } else {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results)) {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    // Read midi file from local
    private void ReadFromFile() {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    // get data from midi file
    private void GetDataFromMidi() {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        foreach (var lane in lanes) {
            lane.SetTimeStamps(array);
        }
        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    // start song
    public void StartSong()
    {
        audioSource.Play();
    }
}
