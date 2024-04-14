using UnityEngine;

public class BrokenPartDisplay : MonoBehaviour
{
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;


    [SerializeField] private GameObject[] Lamps;
    [SerializeField] private GameObject BrokenPart;


    private void Update()
    {
        for (int i = 0; i < Lamps.Length; i++)
        {
            if (BrokenPart.TryGetComponent(out IBrokenParts iBrokenParts) && (i < ((iBrokenParts.Value / iBrokenParts.MaxValue) * Lamps.Length)))
            {
                Lamps[i].GetComponent<MeshRenderer>().material = onMaterial;
            }else
            {
                Lamps[i].GetComponent<MeshRenderer>().material = offMaterial;
            }
        }
    }
}
