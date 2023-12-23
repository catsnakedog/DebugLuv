using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour // Fade In/Out으로 씬 전환을 할 때 사용된다
{                                    // Fade 이펙트를 수정하고 싶으면 여기서 수정하면 된다
    Image _fade;

    public void FadeEffect(string name)
    {
        _fade = transform.Find("Fade").GetComponent<Image>();
        StartCoroutine(FadeEffectC(name));
    }

    IEnumerator FadeEffectC(string name)
    {
        _fade.gameObject.SetActive(true);
        yield return StartCoroutine(FadeInC());
        Managers.Clear();
        SceneManager.LoadScene(name);
        yield return StartCoroutine(FadeOutC());
        _fade.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator FadeInC()
    {
        for(int i = 1; i <= 30; i++)
        {
            _fade.color = new Color(0f, 0f, 0f, i * 1f/30f);
            yield return new WaitForSeconds(1f/30f);
        }
        _fade.color = new Color(0f, 0f, 0f, 1f);
    }

    IEnumerator FadeOutC()
    {
        for (int i = 29; i >= 0; i--)
        {
            _fade.color = new Color(0f, 0f, 0f, i * 1f/30f);
            yield return new WaitForSeconds(1f/30f);
        }
        _fade.color = new Color(0f, 0f, 0f, 0f);
    }
}
