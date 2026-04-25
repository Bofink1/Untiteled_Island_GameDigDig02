using UnityEngine;
using TMPro;

public class TextJumper : MonoBehaviour
{
    public TMP_Text textComponent;
    public float jumpHeight = 10f;
    public float speed = 5f;
    public float waveSpread = 0.5f;

    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            // Skip characters that aren't visible (like spaces)
            if (!charInfo.isVisible) continue;

            // Get the index of the mesh and the first vertex of this character
            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            // Get the vertices of the mesh
            Vector3[] sourceVertices = textInfo.meshInfo[materialIndex].vertices;

            // Calculate the offset using Sine
            // Adding (i * waveSpread) offsets each letter so they jump in a "wave"
            float offset = Mathf.Sin(Time.time * speed + (i * waveSpread)) * jumpHeight;

            // Apply the offset to all 4 vertices of the character (quad)
            Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

            destinationVertices[vertexIndex + 0].y += offset;
            destinationVertices[vertexIndex + 1].y += offset;
            destinationVertices[vertexIndex + 2].y += offset;
            destinationVertices[vertexIndex + 3].y += offset;
        }

        // Push the modified vertices back to the mesh
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}