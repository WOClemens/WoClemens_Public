using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GunInterface
{
    int GetDamag();
    void NotifyShowLine();
    void NotifyHideLine();
    string GetGun();
    bool GetAim();
}
