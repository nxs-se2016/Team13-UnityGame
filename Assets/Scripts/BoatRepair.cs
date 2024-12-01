using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class BoatRepair : MonoBehaviour
{
    public GameObject[] planks;
    public GameObject boat;
    private Vector3 originalPosition;
    public float moveDownDistance = 0.5f;
    public float repairSpeed = 1f;
    
    [System.Serializable]
    public class RepairGroup
    {
        public Material[] damagedMaterials;
        public Material[] repairedMaterials;
        public MeshRenderer[] meshesToRepair;
    }

    public RepairGroup[] repairGroups;

    private Dictionary<string, bool> activatedPlanks = new Dictionary<string, bool>();
    private List<Coroutine> activeCoroutines = new List<Coroutine>();

    private void Start()
    {
        originalPosition = transform.position;
        InitializePlankTracking();
    }

    private void InitializePlankTracking()
    {
        foreach (var plank in planks)
        {
            activatedPlanks[plank.name] = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickuppable") && IsValidPlank(other.name))
        {
            var plankIndex = int.Parse(ExtractNumberFromName(other.name));
            activatedPlanks[other.name] = true;
            StartRepairEffect(plankIndex);
            MovePlateDown();
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickuppable") && IsValidPlank(other.name))
        {
            activatedPlanks[other.name] = false;
            MovePlateUp();
        }
    }

    private bool IsValidPlank(string plankName)
    {
        return activatedPlanks.ContainsKey(plankName);
    }

    private void StartRepairEffect(int groupIndex)
    {
        if (groupIndex >= 0 && groupIndex < repairGroups.Length)
        {
            var repairGroup = repairGroups[groupIndex];
            for (int i = 0; i < repairGroup.meshesToRepair.Length; i++)
            {
                var coroutine = StartCoroutine(LerpMaterial(groupIndex, i));
                activeCoroutines.Add(coroutine);
            }
        }
    }

    private IEnumerator LerpMaterial(int groupIndex, int meshIndex)
    {
        var group = repairGroups[groupIndex];
        Material transitionMaterial = new Material(group.damagedMaterials[meshIndex]);
        group.meshesToRepair[meshIndex].material = transitionMaterial;

        float elapsedTime = 0f;
        
        while (elapsedTime < repairSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / repairSpeed;
            
            transitionMaterial.Lerp(group.damagedMaterials[meshIndex], group.repairedMaterials[meshIndex], t);
            yield return null;
        }

        group.meshesToRepair[meshIndex].material = group.repairedMaterials[meshIndex];
    }

    private string ExtractNumberFromName(string name)
    {
        var match = Regex.Match(name, @"\d+");
        return match.Success ? match.Value : string.Empty;
    }

    private void MovePlateDown()
    {
        transform.position = originalPosition - new Vector3(0, moveDownDistance, 0);
    }

    private void MovePlateUp()
    {
        transform.position = originalPosition;
    }

    private void OnDestroy()
    {
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
    }
}