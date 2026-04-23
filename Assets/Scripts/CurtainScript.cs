using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurtainScript : MonoBehaviour
{
    [SerializeField] private Image curtain;
    [SerializeField] private float fadeTime;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CurtainToggle(1,0,fadeTime));
        }    
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CurtainToggle(0,1,fadeTime));
        }    
    }
    IEnumerator CurtainToggle(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        while(time<duration){
            
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha,endAlpha,time/duration);
            
            Color c =  curtain.color;
            c.a = alpha;
            curtain.color = c;
            
            yield return null;
        }
    }
}
