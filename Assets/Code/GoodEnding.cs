using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoodEnding : MonoBehaviour
{
    public Image fader;

    // Start is called before the first frame update
    void Start()
    {
        fader.CrossFadeAlpha(0,0,false);
        StartCoroutine(EndingFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndingFade()
    {
        yield return new WaitForSeconds(10);
        fader.CrossFadeAlpha(1,5,true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Start Screen");
    }
}
