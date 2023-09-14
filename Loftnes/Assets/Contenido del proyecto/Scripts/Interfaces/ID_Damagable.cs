using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ID_Damagable
{
    public float Health { set; get; }
    public void OnHit(float damage, Vector2 KnockBack);
    public void OnHit(float damage);


}
