using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{

    public GameObject puzzlePiece;
    public GameObject cube;

    float posX;
    float posY;
    float posZ;

    private void Awake()
    {
        posX = transform.position.x;
        posY = 0.5f;
        posZ = transform.position.z;
    }

    private void SpawnPuzzlePart(GameObject parentPiece, int i, int j)
    {
        float deltaX = 0.5f - i;
        float deltaZ = 0.5f - j;

        var boxCollider = parentPiece.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(deltaX, 0.0f, deltaZ);

        Vector3 position = new Vector3(posX + deltaX, posY, posZ + deltaZ);

        Instantiate(cube, position, Quaternion.identity, parentPiece.transform);
    }

    public void CreatePuzzlePiece()
    {
        int[,] matrix = new int[2,2];

        GameObject parentPiece = Instantiate(puzzlePiece, new Vector3(posX, posY, posZ), Quaternion.identity);

        for (int k = 0; k < transform.childCount; k++)
        {
            CreationCube creationCube = transform.GetChild(k).GetComponent<CreationCube>();

            if (creationCube.activated)
            {
                creationCube.SwapState();

                int i = k / 2;
                int j = k % 2;
                matrix[i, j] = 1;

                SpawnPuzzlePart(parentPiece, i, j);
            }
        }

        parentPiece.transform.localScale = new Vector3(0.95f, 1f, 0.95f);
    }

}
