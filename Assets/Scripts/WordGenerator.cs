using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{    
    public int numberToGenerate;

    public List<GameObject> Words;

    private void Awake()
    {
        if(Words.Count > 0)
            GenerateWords();
    }

    public void GenerateWords()
    {
        ClearWords();
        for (int i = 0; i < numberToGenerate; i++)
        {
            int rand = Random.Range(0, Words.Count);
            WordContainer container = Instantiate(Words[rand], this.transform).GetComponent<WordContainer>();            
        }
    }

    public void ClearWords()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
