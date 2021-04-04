using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Projectiles
{
    int GetDamage();

    GameObject target { get; set; }
}
