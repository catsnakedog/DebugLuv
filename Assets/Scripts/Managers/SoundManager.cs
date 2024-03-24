//-------------------------------------------------------------------------------------------------
// @file	SoundManager.cs
//
// @brief	Sound ������ ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : ManagerSingle<SoundManager>, IClearable, IInit // ���带 �����ϴ� Manager�̴�
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // ������ Sound���� AudioSource��
    Dictionary<string, AudioClip> _audioClips = new(); // ����ϴ� ������̴�
    public void Init() // AudioSource�� ������ �ִ� @Sound��� GameObject�� �����
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
            for(int i = 0; i < _soundNames.Length -1 ; i++) // Sound ������ŭ AudioSource�� �߰����ش�
            {
                GameObject audioSource = new GameObject(_soundNames[i]);
                _audioSources[i] = audioSource.AddComponent<AudioSource>();
                audioSource.transform.SetParent(sound.transform);
            }


            _audioSources[(int)Define.Sound.Bgm].loop = true; // BGM���� ��� �ݺ��ž� ������ loop�� ���ش�
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Sfx, float pitch = 1.0f) // ���ϴ� ���带 ����Ѵ� (path)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type); // �ش��ϴ� path�� AudioClip�� ã�´�
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Sfx, float pitch = 1.0f) // ���ϴ� ���带 ����Ѵ� (AudioClip)
    {
        if(audioClip == null)
        {
            Debug.LogWarning($"error_SoundManager : ����ҷ��� audioClip�� ������ϴ�.");
            return;
        }
        if(type == Define.Sound.Bgm) // Bgm�� ��� ������ Bgm�� ���߰� play���ش�
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if(type == Define.Sound.Sfx) // Sfx�� ��� ��ø�� �� �������� PlayOneShot���� ��������ش�
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Sfx];
            audioSource.pitch =  pitch;
            audioSource.PlayOneShot(audioClip);
        }

    }

    public void SetAudioVolume(float volume, Define.Sound type) // Volume�� �������ش�
    {
        _audioSources[(int)type].volume = volume;
    }

    public AudioSource GetAudioSource(Define.Sound type) // AudioSource�� ��ȯ�Ѵ�
    {
        return _audioSources[(int)type];
    }

    AudioClip GetOrAddAudioClip(string name, Define.Sound type = Define.Sound.Sfx) // AudioClip�� ã���ش�
    {
        string path = "";

        if (type == Define.Sound.Bgm)
            path = $"Sounds/Bgm/{name}"; // Bgm���� ��� Sounds/Bgm �� ����ȴ�.
        if (type == Define.Sound.Sfx)
            path = $"Sounds/Sfx/{name}"; // Sfx���� ��� Sounds/Sfx �� ����ȴ�.

        AudioClip audioClip = null;

        if(type == Define.Sound.Bgm) // Bgm���� ��� �ٲ�� ��찡 ���� ���� ������ ���� �����ص��� �ʴ´�
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if(!_audioClips.ContainsKey(name)) // Sfx���� ��� �ݺ� ȣ�� �Ǵ� ��찡 ���� ������ _audioclips�� ���� �������ش�
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips[name] = audioClip;
            }
            else
                audioClip = _audioClips[name];
        }

        if(audioClip == null)
            Debug.LogWarning($"error_SoundManager : {name} Audio ������ �������� �ʽ��ϴ�.");

        return audioClip;
    }

    public void Clear() // ��� AudioSource�� �ʱ�ȭ�Ѵ�
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}
