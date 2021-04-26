using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{

    TextMeshProUGUI text;
    EnemySpawner spawner;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        spawner = FindObjectOfType<EnemySpawner>();
        text.enabled = false;
    }


    IEnumerator DisplayText()
    {
        text.enabled = true;
        text.text = "Wave " + spawner.GetWaveIndex();
        yield return new WaitForSeconds(1.8f);
        text.enabled = false;

    }
    public void Display()
    {
        StartCoroutine(DisplayText());
    }

}
