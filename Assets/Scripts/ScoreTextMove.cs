using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextMove : MonoBehaviour
{
    public float speed;
    public float time;

    Vector3 myVector3 = new Vector3(0f, 0.01f, 0f);

    private void OnEnable()
    {
        StartCoroutine("MoveText");
    }

    IEnumerator MoveText()
    {
        for(int i = 0; i < 50 * time; i++) //총 이동시간은 time만큼일 것
        {
            transform.localPosition += Vector3.up * 0.01f * speed;
            yield return new WaitForSeconds(0.02f); 
        }
        Destroy(gameObject);
    }
}
