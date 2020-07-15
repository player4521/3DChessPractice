using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    // 駒の方向を調節
    private Quaternion orientationWhite = Quaternion.Euler(0, 0, 0);
    private Quaternion orientationDark = Quaternion.Euler(0, 180, 0);

    private void Start()
    {
        SpawnAllChessmans();
    }
    private void Update()
    {
        UpdateSelection();
        DrawChessboard();
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    // 駒の召喚
    private void SpawnChessman(int index, Vector3 position, Quaternion orientation)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], position, orientation) as GameObject;
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }

    // 駒の配置
    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();

        // Light Team
        // King
        SpawnChessman(0, GetTileCenter(3, 0), orientationWhite);
        // Queen
        SpawnChessman(1, GetTileCenter(4, 0), orientationWhite);
        // rooks
        SpawnChessman(2, GetTileCenter(0, 0), orientationWhite);
        SpawnChessman(2, GetTileCenter(7, 0), orientationWhite);
        // bishops
        SpawnChessman(3, GetTileCenter(2, 0), orientationWhite);
        SpawnChessman(3, GetTileCenter(5, 0), orientationWhite);
        // Knights
        SpawnChessman(4, GetTileCenter(1, 0), orientationWhite);
        SpawnChessman(4, GetTileCenter(6, 0), orientationWhite);
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, GetTileCenter(i, 1), orientationWhite);
        }

        // Dark Team
        // King
        SpawnChessman(6, GetTileCenter(3, 7), orientationDark);
        // Queen
        SpawnChessman(7, GetTileCenter(4, 7), orientationDark);
        // rooks
        SpawnChessman(8, GetTileCenter(0, 7), orientationDark);
        SpawnChessman(8, GetTileCenter(7, 7), orientationDark);
        // bishops
        SpawnChessman(9, GetTileCenter(2, 7), orientationDark);
        SpawnChessman(9, GetTileCenter(5, 7), orientationDark);
        // Knights
        SpawnChessman(10, GetTileCenter(1, 7), orientationDark);
        SpawnChessman(10, GetTileCenter(6, 7), orientationDark);
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, GetTileCenter(i, 6), orientationDark);
        }
    }
    // タイルのセンターを取得
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
    // チェスボード配置
    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heighLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * i;
                Debug.DrawLine(start, start + heighLine);

            }
        }

        // チェスボードラインをデバッグで確認
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
}
