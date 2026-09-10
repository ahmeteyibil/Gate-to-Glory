using System;
using UnityEngine;

public class PlayerHealthController : BaseHealthController
{
    [SerializeField] GameObject playerDeathEffectPrefab;

    protected override void Die()
    {
        Destroy(this.gameObject);
        var deathEffect = Instantiate(playerDeathEffectPrefab, this.transform.position, this.transform.localRotation);
        Destroy(deathEffect, .5f);
    }
}
