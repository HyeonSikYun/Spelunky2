using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect2 : MonoBehaviour
{
    public float scaleSpeed = 0.05f; // �������� �����ϴ� �ӵ�
    public Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 targetScale = new Vector3(1.0f, 1.0f, 1.0f);
    private bool scalingUp = true; // ������ �� ũ�⸦ Ű��� �÷���
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = startScale; // ���� ������ ����
    }
    private void Update()
    {
        if (scalingUp)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            //transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
            if (transform.localScale.x >= targetScale.x)
                scalingUp = false; // ��ǥ �����Ͽ� �����ϸ� ũ�� Ű��� ����
        }
    }

    void Delete()
    {
        gameObject.SetActive(false);

    }
}
