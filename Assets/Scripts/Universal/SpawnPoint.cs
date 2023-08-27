using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum WhatEnemy
    {
        heavyEnemy,
        placeHolder0,
        placeHolder1,
        placeHolder2,
    }

    public WhatEnemy _WhatEnemy;

    public GameObject[] _Enemies;
    int _WhatEnemyInt;

    private void Start()
    {
        switch (_WhatEnemy)
        {
            case WhatEnemy.heavyEnemy:
                _WhatEnemyInt = 0;
                break;
            case WhatEnemy.placeHolder0:
                _WhatEnemyInt = 1;
                break;
            case WhatEnemy.placeHolder1:
                _WhatEnemyInt = 2;
                break;
            case WhatEnemy.placeHolder2:
                _WhatEnemyInt = 3;
                break;
            default:
                break;
        }
    }
    public void SpawnEnemy()
    {
        Instantiate(_Enemies[_WhatEnemyInt], transform.position,transform.rotation);
    }
}
