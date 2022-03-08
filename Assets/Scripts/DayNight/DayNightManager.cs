using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    #region Singleton
    public static DayNightManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    [SerializeField]
    private float _transitionTime = 1f;
    [SerializeField]
    private AnimationCurve _dayCurve;
    [SerializeField]
    private AnimationCurve _nightCurve;

    private float _lightIntensity;
    private AnimationCurve _currentCurve;

    public delegate void SetIntensity(float percentage);
    public event SetIntensity SetLightIntensity;

    private void Start()
    {
        _currentCurve = _dayCurve;
        ChangeTime();
    }

    public void ChangeTime()
    {
        _currentCurve = (_currentCurve == _dayCurve) ? _nightCurve : _dayCurve;
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        float timer = 0f;
        while(timer < _transitionTime) {
            _lightIntensity = timer / _transitionTime;
            SetLightIntensity?.Invoke(_currentCurve.Evaluate(_lightIntensity));
            timer += Time.deltaTime;
            yield return null;
        }
        ChangeTime();
    }
}
