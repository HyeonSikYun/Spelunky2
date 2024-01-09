using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    private Transform player; // �÷��̾��� Transform
    private Transform exit; // �ⱸ�� Transform
    public RectTransform arrowImage; // ȭ��ǥ �̹����� RectTransform


    private void Start()
    {
        player = GameObject.Find("Player").transform; // "PlayerObjectName"�� �÷��̾� ������Ʈ�� �̸��Դϴ�.
        exit = GameObject.Find("Volcano_Exit").transform;
    }

    private void Update()
    {
        if (player == null || exit == null || arrowImage == null)
            return;

        // �ⱸ ���� ���� ���
        Vector3 direction = exit.position - player.position;
        direction.z = 0f; // 2D ���ӿ����� Z ���� ������� �ʽ��ϴ�
        //Debug.Log(direction);
        // ���� ���ͷκ��� ���� ���
        float angle = Vector3.SignedAngle(-Vector3.up, direction, Vector3.forward);

        // ȭ��ǥ �̹����� Z ȸ������ ����
        arrowImage.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
