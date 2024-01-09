using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime = 2.0f; // �Ѿ��� ������ �ð� (��: 2��)
    private float currentLifetime = 0.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        currentLifetime += Time.deltaTime;

        // �Ѿ��� ���� lifetime�� �Ѿ��� �����ؾ� �ϴ� �ð����� ũ�ٸ� �Ѿ��� �ı�
        if (currentLifetime >= bulletLifetime)
        {
            Destroy(gameObject);
        }
    }
}
