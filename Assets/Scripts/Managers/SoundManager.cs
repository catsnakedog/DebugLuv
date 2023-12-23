using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager // ���带 �����ϴ� Manager�̴�
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // ������ Sound���� AudioSource��
    Dictionary<string, AudioClip> _audioclips = new Dictionary<string, AudioClip>(); // ����ϴ� ������̴�
    public void Init() // AudioSource�� ������ �ִ� @Sound��� GameObject�� �����
    {
        GameObject go = GameObject.Find("@Sound");
        if(go==null)
        {
            go = new GameObject{name = "@Sound"};
            Object.DontDestroyOnLoad(go);

            string[] _soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < _soundNames.Length -1 ; i++) // Sound ������ŭ AudioSource�� �߰����ش�
            {
                GameObject go2 = new GameObject {name = _soundNames[i]};
                _audioSources[i] = go2.AddComponent<AudioSource>();
                go2.transform.parent = go.transform;
            }


            _audioSources[(int)Define.Sound.Bgm].loop = true; // BGM���� ��� �ݺ��ž� ������ loop�� ���ش�
        }
    }

    public void Clear() // ��� AudioSource�� �ʱ�ȭ�Ѵ�
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioclips.Clear();
    }
    public void Play(string path, Define.Sound type = Define.Sound.Sfx, float pitch = 1.0f) // ���ϴ� ���带 ����Ѵ� (path)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type); // �ش��ϴ� path�� AudioClip�� ã�� 
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Sfx, float pitch = 1.0f) // ���ϴ� ���带 ����Ѵ� (AudioClip)
    {
        if(audioClip == null)
        {
            Debug.LogWarning("No Clip");
            return;
        }
        if(type == Define.Sound.Bgm) // Bgm�� ��� ������ Bgm�� ���߰� play���ش�
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch= pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if(type == Define.Sound.Sfx) // Sfx�� ��� ��ø�� �� �������� PlayOneShot���� ��������ش�
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Sfx];
            audioSource.pitch=  pitch;
            audioSource.PlayOneShot(audioClip);
        }

    }

    public void SetAudioVolume(float volume,Define.Sound type) // Volume�� �������ش�
    {
        _audioSources[(int)type].volume = volume;
    }

    public AudioSource GetAudioSource(Define.Sound type) // AudioSource�� ��ȯ�Ѵ�
    {
        return _audioSources[(int)type];
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Sfx) // AudioClip�� ã���ش�
    {
        if(path.Contains("Sounds/") == false) // Sound���� ��� Resources/Sounds�� ����ȴ�
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if(type == Define.Sound.Bgm) // Bgm���� ��� �ٲ�� ��찡 ���� ���� ������ ���� �����ص��� �ʴ´�
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if(_audioclips.TryGetValue(path, out audioClip) == false) // Sfx���� ��� �ݺ� ȣ�� �Ǵ� ��찡 ���� ������ _audioclips�� ���� �������ش�
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioclips.Add(path, audioClip);
            }
        }

        if(audioClip == null)
            Debug.LogWarning($"AudioClip Missing ! {path}");

        
        return audioClip;
    }
}
