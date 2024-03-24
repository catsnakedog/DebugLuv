//-------------------------------------------------------------------------------------------------
// @file	SoundManager.cs
//
// @brief	Sound 관리를 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : ManagerSingle<SoundManager>, IClearable, IInit // 사운드를 관리하는 Manager이다
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // 각각의 Sound들의 AudioSource들
    Dictionary<string, AudioClip> _audioClips = new(); // 출력하는 오디오이다
    public void Init() // AudioSource를 가지고 있는 @Sound라는 GameObject를 만든다
    {
        GameObject sound = GameObject.Find("@Sound");
        if(sound == null)
        {
            GameObject root = GameObject.Find("@Root");
            if (root == null)
                root = new GameObject("@Root");

            sound = new GameObject{name = "@Sound"};
            sound.transform.SetParent(root.transform);

            string[] _soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < _soundNames.Length -1 ; i++) // Sound 종류만큼 AudioSource를 추가해준다
            {
                GameObject audioSource = new GameObject(_soundNames[i]);
                _audioSources[i] = audioSource.AddComponent<AudioSource>();
                audioSource.transform.SetParent(sound.transform);
            }


            _audioSources[(int)Define.Sound.Bgm].loop = true; // BGM같은 경우 반복돼야 함으로 loop를 켜준다
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Sfx, float pitch = 1.0f) // 원하는 사운드를 출력한다 (path)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type); // 해당하는 path의 AudioClip을 찾는다
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Sfx, float pitch = 1.0f) // 원하는 사운드를 출력한다 (AudioClip)
    {
        if(audioClip == null)
        {
            Debug.LogWarning($"error_SoundManager : 재생할려는 audioClip이 비었습니다.");
            return;
        }
        if(type == Define.Sound.Bgm) // Bgm인 경우 기존의 Bgm을 멈추고 play해준다
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if(type == Define.Sound.Sfx) // Sfx인 경우 중첩될 수 있음으로 PlayOneShot으로 실행시켜준다
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Sfx];
            audioSource.pitch =  pitch;
            audioSource.PlayOneShot(audioClip);
        }

    }

    public void SetAudioVolume(float volume, Define.Sound type) // Volume을 세팅해준다
    {
        _audioSources[(int)type].volume = volume;
    }

    public AudioSource GetAudioSource(Define.Sound type) // AudioSource를 반환한다
    {
        return _audioSources[(int)type];
    }

    AudioClip GetOrAddAudioClip(string name, Define.Sound type = Define.Sound.Sfx) // AudioClip을 찾아준다
    {
        string path = "";

        if (type == Define.Sound.Bgm)
            path = $"Sounds/Bgm/{name}"; // Bgm같은 경우 Sounds/Bgm 에 저장된다.
        if (type == Define.Sound.Sfx)
            path = $"Sounds/Sfx/{name}"; // Sfx같은 경우 Sounds/Sfx 에 저장된다.

        AudioClip audioClip = null;

        if(type == Define.Sound.Bgm) // Bgm같은 경우 바뀌는 경우가 별로 없기 때문에 따로 저장해두진 않는다
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if(!_audioClips.ContainsKey(name)) // Sfx같은 경우 반복 호출 되는 경우가 많기 때문에 _audioclips에 따로 저장해준다
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips[name] = audioClip;
            }
            else
                audioClip = _audioClips[name];
        }

        if(audioClip == null)
            Debug.LogWarning($"error_SoundManager : {name} Audio 파일이 존재하지 않습니다.");

        return audioClip;
    }

    public void Clear() // 모든 AudioSource를 초기화한다
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}
