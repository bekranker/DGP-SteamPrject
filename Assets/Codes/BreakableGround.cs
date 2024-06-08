using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreakableGround : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField]  private float _shakeDuration;
    [SerializeField]  private float _shakeForce;
    [SerializeField] private int _shakeCount;
    [SerializeField] private bool _fadeOut;
    [SerializeField] private bool _delay;
    [SerializeField] private bool _faded;
    [SerializeField] private ParticleSystem _touchedVFX;

    private bool _canBreake = true;


    void Awake()
    {
        _canBreake = true;
    }
    void Start()
    {
        if(_faded)
        {
            GetComponent<Collider2D>().enabled = false;
            _spriteRenderer.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!_canBreake) return;
        if (other.gameObject.tag == "Player")
        {
            if (IsGrounded())
            {
                BreakIt();
                _canBreake= false;
            }
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(!_canBreake) return;
        if (other.gameObject.tag == "Player")
        {
            if (IsGrounded())
            {
                BreakIt();
                _canBreake= false;
            }
        }
    }
    private void BreakIt()
    {
        DOTween.Kill(_spriteRenderer.transform);
        Instantiate(_touchedVFX, transform.position + Vector3.down, Quaternion.identity);
        BreakMe.ShakeObject(1, _spriteRenderer.transform, _shakeForce, _shakeDuration, _delay, () => 
        {
            _spriteRenderer.enabled = false; 
            GetComponent<Collider2D>().enabled = false;
        }); 
    }
    private bool IsGrounded()
    {
        // Bu metod karakterin gerçekten zemin üzerinde olup olmadığını kontrol etmeli
        // Burada basit bir Physics2D raycast veya overlap check kullanılabilir
        // Örneğin:
        Collider2D col = Physics2D.OverlapBox(transform.position + new Vector3(0f, 0.1f, 0f), transform.localScale, 0f, LayerMask.GetMask("Player"));
        return col != null;
    }
    public void GetMe()
    {
        _spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        BreakMe.ShakeObject(_shakeCount, _spriteRenderer.transform, _shakeForce, _shakeDuration, _delay);
        _canBreake = true;
    }
}