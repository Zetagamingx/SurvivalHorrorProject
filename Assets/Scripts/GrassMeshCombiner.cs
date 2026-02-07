using UnityEngine;
using System.Collections.Generic;

public class GrassMeshCombiner : MonoBehaviour
{
    [ContextMenu("Combine Grass Meshes")]
    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        List<CombineInstance> combineList = new List<CombineInstance>();

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.sharedMesh == null || mf.gameObject == this.gameObject)
                continue;

            CombineInstance ci = new CombineInstance();
            ci.mesh = mf.sharedMesh;
            ci.transform = mf.transform.localToWorldMatrix;
            combineList.Add(ci);
        }

        // Create combined mesh
        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // in case of lots of vertices
        combinedMesh.CombineMeshes(combineList.ToArray(), true, true);

        // Create a new GameObject to hold the combined mesh
        GameObject combinedGO = new GameObject("Combined_GrassMesh");
        combinedGO.transform.position = Vector3.zero;

        MeshFilter mfCombined = combinedGO.AddComponent<MeshFilter>();
        mfCombined.mesh = combinedMesh;

        MeshRenderer mrCombined = combinedGO.AddComponent<MeshRenderer>();
        mrCombined.sharedMaterial = meshFilters[1].GetComponent<MeshRenderer>().sharedMaterial;

        // Optional: mark as static
        combinedGO.isStatic = true;

        Debug.Log("Grass meshes combined successfully!");

        // Disable original grass prefabs
        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.gameObject != this.gameObject)
                mf.gameObject.SetActive(false);
        }
    }
}