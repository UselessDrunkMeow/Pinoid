using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualHealth : MonoBehaviour
{
    public Image[] _Hearts;
    public Sprite _FullHeart;
    public Sprite _EmptyHeart;

    [SerializeField] Health _HealthScript;
    void Start()
    {

    }


    void Update()
    {
        for (int i = 0; i < _Hearts.Length; i++)
        {
            if(i < _HealthScript._CurrentHealthPoints)
            {
                _Hearts[i].sprite = _FullHeart;
            }
            else
            {
                _Hearts[i].sprite = _EmptyHeart;
            }


            if (i < _HealthScript._MaxHealthPoints)
            {
                _Hearts[i].enabled = true;
            }
            else
            {
                _Hearts[i].enabled = false;
            }
        }
    }
}
