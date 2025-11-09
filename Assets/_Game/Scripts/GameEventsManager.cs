using System;
using System.Collections;
using UnityCommunity.UnitySingleton;
using UnityEngine;

public class GameEventsManager : PersistentMonoSingleton<GameEventsManager>
{

    [Header("***Elements***")]
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private Transform enemyContainer;

    [Header("***Settings***")]
    [SerializeField] private float movementFactorWhenSlowedDown = 0.4f;
    [SerializeField] private float maxSlowDownTime = 1f;
    [SerializeField] private float remainingSlowDownTime;


    private void Start()
    {
        remainingSlowDownTime = maxSlowDownTime;
    }
    private void OnEnable()
    {
        InputController.SlowDownActivatedAction += SetSlowdown;
    }

    private void OnDisable()
    {
        InputController.SlowDownActivatedAction -= SetSlowdown;
    }
    private void SetSlowdown(bool activated)
    {
        if (!activated)
        {
            // yavaþlama kapandýðýnda hýzlarý eski haline döndür
            SetEnemySpeeds(1, 1);
            return;
        }

        // aktifse ve süresi varsa yavaþlat
        if (remainingSlowDownTime > 0f)
        {
            SetEnemySpeeds(movementFactorWhenSlowedDown, 0.1f);
            remainingSlowDownTime -= 0.1f;
        }
        else
        {
            // süre bittiðinde de kapatmayý burada yap
            SetEnemySpeeds(1,1);
        }
    }

    public void SetEnemySpeeds(float factor, float duration)
    {
        var enemies = enemyContainer.GetComponentsInChildren<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.SlowDown(movementFactorWhenSlowedDown, duration);
        }
    }

    private IEnumerator SlowVisualEffect(float duration)
    {
        yield return null;
    }




}
