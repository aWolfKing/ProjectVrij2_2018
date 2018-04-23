using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable_old {

    void Register();

    void Damage(float amount);

    bool CheckCollison(params SphereHitbox[] hitboxes);

}
