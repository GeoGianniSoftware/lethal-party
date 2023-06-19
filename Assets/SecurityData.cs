using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SecurityData
{
    public SecurityLevels secLEVEL;

}

[System.Serializable]
public class SecurityAreaData
{
    public SecurityLevels secLEVEL;
    public List<Transform> patrolPoints;
    public List<Transform> guardPoints;

}
public enum SecurityLevels
{
    PUBLIC,
    EMPLOY,
    PRIVATE,
    HIGHSECURITY
}
