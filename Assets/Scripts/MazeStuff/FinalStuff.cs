using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStuff : MonoBehaviour
{
    public ScenarioMiddle     scenario;
    
    private void OnTriggerEnter(Collider other)
    {
        scenario.WinScenario();
    }
}
