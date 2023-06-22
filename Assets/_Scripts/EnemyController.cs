using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Character Character;
    [SerializeField] List<LightSwitch> lightSwitches;
    [SerializeField] LightSwitch lightSwitch;

    [Header("Navigation")]
    NavMeshAgent enemyNavigation;
    [SerializeField] Transform selectedDestination;
    [SerializeField] Transform[] moveLocations;
    [SerializeField] bool hasReachedDestination = true;
    [SerializeField] float waitTimeAtEachLocation = 5f;

    Coroutine coroutine = null;
    bool onTriggerCoroutine = false;
    private void Awake()
    {
        enemyNavigation = GetComponent<NavMeshAgent>();

        coroutine = null;
        onTriggerCoroutine = false;
    }

    private void Update()
    {
            if (hasReachedDestination)
            {
                hasReachedDestination = false;
                ChooseRandomLocation();
                enemyNavigation.destination = selectedDestination.position;
            }
            else if (Vector3.Distance(this.transform.position, selectedDestination.position) < 0.2)
            {
                if (coroutine == null)
                {
                    coroutine = StartCoroutine(WaitAtLocation());
                }
            }
    }

    void ChooseRandomLocation()
    {
        int rand = Random.Range(0, moveLocations.Length);
        selectedDestination = moveLocations[rand];
    }


    IEnumerator WaitAtLocation()
    {
        yield return new WaitForSeconds(waitTimeAtEachLocation);
        hasReachedDestination = true;
        coroutine = null;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            switch (col.gameObject.name)
            {
                case "Kitchen": lightSwitch = lightSwitches[0]; break;
                case "LivingRoom": lightSwitch = lightSwitches[1]; break;
                case "Bathroom": lightSwitch = lightSwitches[2]; break;
                case "Bedroom": lightSwitch = lightSwitches[3]; break;
                case "StorageRoom": lightSwitch = lightSwitches[4]; break;
                default: Debug.Log("No lightswitch found!"); break;
            }

            Transform tempDestination = selectedDestination;

            if (!lightSwitch.isOn)
            {
                selectedDestination = lightSwitch.transform;
                if(Vector3.Distance(this.transform.position, lightSwitch.transform.position) < 0.25)
                {
                    lightSwitch.isOn = true;
                    selectedDestination = tempDestination;
                }
            }
        }
    }

    private IEnumerator OnTriggerExit(Collider col)
    {
        if (onTriggerCoroutine == false)
        {
            onTriggerCoroutine = true;
            if (col.CompareTag("Player"))
            {
                enemyNavigation.destination = this.transform.position;
                this.transform.LookAt(col.transform);
                yield return new WaitForSeconds(1f);
                enemyNavigation.destination = col.transform.position;

                if (Vector3.Distance(this.transform.position, selectedDestination.position) > 0.1)
                {
                    enemyNavigation.destination = selectedDestination.position;
                }
            }
            onTriggerCoroutine = false;
        }
    }
}
enum Character
{
    Dad,
    Mom,
    OlderChild,
    Dog
}