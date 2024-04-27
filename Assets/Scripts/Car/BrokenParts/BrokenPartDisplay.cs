using UnityEngine;

public class BrokenPartDisplay : MonoBehaviour
{
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;


    [SerializeField] private GameObject[] Lamps;
    [SerializeField] private GameObject BrokenPart;


    private void Update()
    {
        if (BrokenPart.TryGetComponent(out IBrokenParts iBrokenParts))
        {
            for (int i = 0; i < Lamps.Length; i++)
            {
                if ((i < ((iBrokenParts.Value / iBrokenParts.MaxValue) * Lamps.Length)))
                {
                    Lamps[i].GetComponent<MeshRenderer>().material = onMaterial;
                }
                else
                {
                    Lamps[i].GetComponent<MeshRenderer>().material = offMaterial;
                }
            }
        }
        if(BrokenPart.TryGetComponent(out ItemValue itemValue))
        {
            for (int i = 0; i < Lamps.Length; i++)
            {
                if ((i < ((itemValue.Value / itemValue.StartValue) * Lamps.Length)))
                {
                    Lamps[i].GetComponent<MeshRenderer>().material = onMaterial;
                }
                else
                {
                    Lamps[i].GetComponent<MeshRenderer>().material = offMaterial;
                }
            }
        }
    }
}
