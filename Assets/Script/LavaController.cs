using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    private bool isVisible = false; // ����� �þ߿� ���̴��� ���θ� ��Ÿ���� ����
    private SpriteRenderer renderer; // ����� ������ ������Ʈ

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // �þ߿� ����� ���̴��� ���θ� üũ
        if (IsVisibleInCamera())
        {
            isVisible = true;
            Debug.Log("���δ�");
        }
        else
        {
            isVisible = false;
            Debug.Log("�Ⱥ��δ�");
        }

        // isVisible ���� ���� ��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        gameObject.SetActive(isVisible);
    }

    private bool IsVisibleInCamera()
    {
        // ����� �߽� ��ġ�� �������� �þ߿� ���̴��� üũ
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return !GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
