using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _MaxHealthPoints;
    public int _CurrentHealthPoints;
    Color _NewColor;
    Color _SpriteColor;
    SpriteRenderer _Sprite;
    [SerializeField] float _ITime;
    float _Timer;
    bool _Invincible = false;
    bool _IsChangeColor = false;

    void Start()
    {
        _Sprite = GetComponentInChildren<SpriteRenderer>();

        _SpriteColor = _Sprite.color;
        _CurrentHealthPoints = _MaxHealthPoints;
    }

    void Update()
    {
        DebugStuff();
        if (_Invincible)
        {
            IFrames();
            _Timer += Time.deltaTime;
        }
        else
        {
            StopAllCoroutines();
            _Sprite.color = _SpriteColor;
        }
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
            _Invincible = true;
            StopCoroutine(HealingColor());
            StartCoroutine(DamageColor());
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
            StartCoroutine(HealingColor());
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
            if (!_IsChangeColor)
                StartCoroutine(DamageColor());
        }
        else
        {
            StopCoroutine(DamageColor());
            _Invincible = false;
            _IsChangeColor = false;
            _Timer = 0;
        }
    }
    IEnumerator DamageColor()
    {
        _IsChangeColor = true;
        _Sprite.color = _NewColor;
        yield return new WaitForSeconds(0.1f);
        _Sprite.color = _SpriteColor;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DamageColor());
    }
    IEnumerator HealingColor()
    {
        print("Heal!");
        _Sprite.color = _NewColor;
        yield return new WaitForSeconds(0.1f);
        print("Heal!");
        _Sprite.color = _SpriteColor;
        yield return new WaitForSeconds(0.1f);
        print("Heal!");
        _Sprite.color = _NewColor;
        yield return new WaitForSeconds(0.1f);
        print("Heal!");
        _Sprite.color = _SpriteColor;
        yield return new WaitForSeconds(0.1f);
        print("Heal!");
        _Sprite.color = _NewColor;
        yield return new WaitForSeconds(0.1f);
        print("Heal!");
        _Sprite.color = _SpriteColor;
        yield return new WaitForSeconds(0.1f);
    }
}
