using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// logo , hood, lights, Wheels, Mirrors, Top
public enum ENAudioTrack { None, Vocals, Bass, Drums, Kick, FX, Synth }

public class TrackMuteController : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private TimelineAsset timelineAsset;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (playableDirector != null)
        {
            timelineAsset = (TimelineAsset)playableDirector.playableAsset;
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        MuteAll();
        playableDirector.stopped -= OnPlayableDirectorStopped; // Unsubscribe to avoid multiple calls
    }

    public void MuteAll()
    {
        if (timelineAsset == null)
        {
            return;
        }

        foreach (var track in timelineAsset.GetOutputTracks())
        {
            if (track is AudioTrack audioTrack)
            {
                SetTrackMute(audioTrack, true);
            }
        }
    }


    public void RequestTrackState(ENAudioTrack InAudioTrack, bool InState)
    {




        //if (timelineAsset == null)
        //{
        //    return;
        //}

        //foreach (var track in timelineAsset.GetOutputTracks())
        //{
        //    if (track is AudioTrack audioTrack)
        //    {
        //        switch (InAudioTrack)
        //        {
        //            case ENAudioTrack.None:
        //                break;
        //            case ENAudioTrack.Vocals:
        //                print($"Request Track State - Vocals: {(InState ? "Mute" : "Unmute")}");
        //                if (audioTrack.name == "Vocals") SetTrackMute(audioTrack, InState);
        //                break;
        //            case ENAudioTrack.Bass:
        //                print($"Request Track State - Bass: {(InState ? "Mute" : "Unmute")}");
        //                if (audioTrack.name == "Bass") SetTrackMute(audioTrack, InState);
        //                break;
        //            case ENAudioTrack.Drums:
        //                print($"Request Track State - Drums: {(InState ? "Mute" : "Unmute")}");
        //                if (audioTrack.name == "Drums") SetTrackMute(audioTrack, InState);
        //                break;
        //            case ENAudioTrack.Kick:
        //                print($"Request Track State - Kick: {(InState ? "Mute" : "Unmute")}");
        //                if (audioTrack.name == "Kick") SetTrackMute(audioTrack, InState);
        //                break;
        //            case ENAudioTrack.FX:
        //                print($"Request Track State - FX: {(InState ? "Mute" : "Unmute")}");
        //                if (audioTrack.name == "FX") SetTrackMute(audioTrack, InState);
        //                break;
        //            case ENAudioTrack.Synth:
        //                print($"Request Track State - Synth: {(InState ? "Mute" : "Unmute")}");
        //                if (audioTrack.name == "Synth") SetTrackMute(audioTrack, InState);
        //                break;
        //        }
        //    }
        //}
    }


    private void SetTrackMute(AudioTrack audioTrack, bool mute)
    {


    }

}
