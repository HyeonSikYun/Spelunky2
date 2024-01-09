using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip[] levelMusicClips; // 각 레벨에 해당하는 배경음악 클립들을 저장

    private int currentLevel = 1; // 현재 레벨을 추적하는 변수
    private AudioClip currentClip; // 현재 재생 중인 클립을 추적


    void Start()
    {
        PlayLevelMusic();
    }

    // 레벨에 따른 배경음악 설정 및 재생
    public void PlayLevelMusic()
    {
        if (currentLevel >= 1 && currentLevel <= levelMusicClips.Length)
        {
            if (currentClip != null)
            {
                bgm.Stop(); // 이전 레벨의 음악 멈춤
            }
            if (currentLevel - 1 < levelMusicClips.Length) // 추가된 부분
            {
                currentClip = levelMusicClips[currentLevel - 1];
                bgm.clip = currentClip;
                bgm.Play();
            }
            else
            {
                Debug.LogError("No music clip for level " + currentLevel);
            }
        }
    }

    // 현재 레벨을 설정하는 함수
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        PlayLevelMusic();
        Debug.Log("SetCurrentLevel - currentLevel: " + currentLevel);
    }
}
