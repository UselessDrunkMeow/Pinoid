using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _MaxHealthPoints;
    public int _CurrentHealthPoints;
    Color _NewColor;
    Color _SpriteColor;
    [SerializeField] SpriteRenderer _Sprite;
    [SerializeField] float _ITime;
    float _Timer;
    [SerializeField] bool _Invincible;

    void Start()
    {
        _Sprite = GetComponentInChildren<SpriteRenderer>();

        _SpriteColor = _Sprite.color;
        _CurrentHealthPoints = _MaxHealthPoints;
    }

    void Update()
    {
        DebugStuff();
    }

    public void DecreaseHealth(int damage)
    {
        if (!_Invincible)
        {
            _CurrentHealthPoints -= damage;
            _NewColor.r = 255;
            _NewColor.g = 0;
            _NewColor.b = 0;
            _NewColor.a = 255;
            IFrames();
        }
    }

    public void IncreaseHealth(int healing)
    {
        if (!_Invincible)
        {
            _CurrentHealthPoints += healing;
            _NewColor.r = 0;
            _NewColor.g = 255;
            _NewColor.b = 0;
            _NewColor.a = 255;
            IFrames();
        }
    }

    void DebugStuff()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            DecreaseHealth(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            IncreaseHealth(1);
    }
    void IFrames()
    {
        if (_Timer < _ITime)
        {
            _Invincible = true;
            ChangeColor();
        }
        else
            StopCoroutine(ChangeColor());
            _Invincible = false;
    }
    IEnumerator ChangeColor()
    {
        _Sprite.color = _NewColor;
        yield return new WaitForSeconds(0.05f);
        _Sprite.color = _SpriteColor;
        yield return new WaitForSeconds(0.05f);
        ChangeColor();

    }
}
