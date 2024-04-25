using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;

    public void GenerateLabyrinth () {
        // przechodzimy piksel po pikselu i odpowiednia rzecz umieszczamy na scenie
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) GenerateTile(x, y);
        }
    }

    public Material [] colorMaterials;

    public void ColorLabyrinth () {
        foreach (Transform child in transform) {
            if (child.tag == "Wall") {
                int picked = Random.Range(0, colorMaterials.Length);
                foreach (Transform grandChild in child.transform) {
                    // Renderer r = grandChild.GetComponent<Renderer>();
                    if (grandChild.GetComponent<Renderer>() != null) grandChild.GetComponent<Renderer>().material = colorMaterials[picked];
                }
            }
        }
    }

    public void DeleteLabyrinth () {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void GenerateTile (int x, int z) {
        Color pixelColor = map.GetPixel(x, z);

        // czy wybrany pixel jest przeźroczysty?
        if (pixelColor.a == 0) {
            return;
        }

        foreach (ColorToPrefab cM in colorMappings) {
            if (cM.color.Equals(pixelColor)) {
                Vector3 position = new Vector3 (x, 0, z) * offset;
                Instantiate(cM.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}

// [CustomEditor(typeof(LevelGenerator))]
// public class EditorButton : Editor {
//     public override void OnInspectorGUI()
//     {
//         // rysujemy defaultowy inspektor
//         DrawDefaultInspector();

//         LevelGenerator generator = (LevelGenerator)target;

//         // dodajemy przycisk i sprawdzamy, czy zosta kliknięty
//         if (GUILayout.Button("Create Labyrinth")) generator.GenerateLabyrinth();
//         if (GUILayout.Button("Color Labyrinth")) generator.ColorLabyrinth();
//         if (GUILayout.Button("Delete Labyrinth")) generator.DeleteLabyrinth();
//     }
// }