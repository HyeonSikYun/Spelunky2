using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip[] levelMusicClips; // �� ������ �ش��ϴ� ������� Ŭ������ ����

    private int currentLevel = 1; // ���� ������ �����ϴ� ����
    private AudioClip currentClip; // ���� ��� ���� Ŭ���� ����


    void Start()
    {
        PlayLevelMusic();
    }

    // ������ ���� ������� ���� �� ���
    public void PlayLevelMusic()
    {
        if (currentLevel >= 1 && currentLevel <= levelMusicClips.Length)
        {
            if (currentClip != null)
            {
                bgm.Stop(); // ���� ������ ���� ����
            }
            if (currentLevel - 1 < levelMusicClips.Length) // �߰��� �κ�
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

    // ���� ������ �����ϴ� �Լ�
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        PlayLevelMusic();
        Debug.Log("SetCurrentLevel - currentLevel: " + currentLevel);
    }
}
