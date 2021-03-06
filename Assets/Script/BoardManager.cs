﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] AllowedMoves { set; get; }

    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Material previousMat;
    public Material selectedMat;

    public int[] EnPassantMove { set; get; }

    // 駒の方向を調節
    private Quaternion orientationLight = Quaternion.Euler(0, 0, 0);
    private Quaternion orientationDark = Quaternion.Euler(0, 180, 0);

    public bool isLightTurn = true;

    private void Start()
    {
        Instance = this;
        SpawnAllChessmans();
    }
    private void Update()
    {
        UpdateSelection();
        DrawChessboard();

        // マウスの右クリック
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {

                // 選択された駒が居ない場合
                if (selectedChessman == null)
                {
                    // 駒を選択
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    // 駒を移動
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
    }

    // 駒を選択
    private void SelectChessman(int x, int y)
    {
        if (Chessmans[x, y] == null)
            return;

        if (Chessmans[x, y].isLightSide != isLightTurn)
            return;

        bool hasAtleastOneMove = false;
        AllowedMoves = Chessmans[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (AllowedMoves[i, j])
                {
                    hasAtleastOneMove = true;
                }
            }
        }

        if (!hasAtleastOneMove)
        {
            return;
        }

        selectedChessman = Chessmans[x, y];
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighlightAllowedMoves(AllowedMoves);
    }

    // 駒を移動
    private void MoveChessman(int x, int y)
    {
        // x,yに移動できる場合
        if (AllowedMoves[x, y])
        {
            Chessman c = Chessmans[x, y];

            if (c != null && c.isLightSide != isLightTurn)
            {

                // Kingの場合はゲーム終了
                if (c.GetType() == typeof(King))
                {
                    EndGame();
                    return;
                }

                // King以外の場合は削除
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            if (x == EnPassantMove[0] && y == EnPassantMove[1])
            {
                if (isLightTurn)
                {
                    c = Chessmans[x, y - 1];
                }
                else
                {
                    c = Chessmans[x, y - 1];
                }
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;
            if (selectedChessman.GetType() == typeof(Pawn))
            {
                if (y == 7)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman);
                    SpawnChessman(1, x, y, orientationLight);
                    selectedChessman = Chessmans[x, y];
                }
                else if (y == 0)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman);
                    SpawnChessman(7, x, y, orientationDark);
                }

                if (selectedChessman.CurrentY == 1 && y == 3)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }
                else if (selectedChessman.CurrentY == 6 && y == 4)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }
            }

            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            isLightTurn = !isLightTurn;
        }

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            return;
        }

        // 衝突が感知された領域
        RaycastHit hit;

        // マウスの近くにオブジェクトがあるかを確認
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
    private void SpawnChessman(int index, int x, int y, Quaternion orientation)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), orientation) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    // 駒の配置
    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        EnPassantMove = new int[2] { -1, -1 };

        // Light Team
        // King
        SpawnChessman(0, 3, 0, orientationLight);
        // Queen
        SpawnChessman(1, 4, 0, orientationLight);
        // rooks
        SpawnChessman(2, 0, 0, orientationLight);
        SpawnChessman(2, 7, 0, orientationLight);
        // bishops
        SpawnChessman(3, 2, 0, orientationLight);
        SpawnChessman(3, 5, 0, orientationLight);
        // Knights
        SpawnChessman(4, 1, 0, orientationLight);
        SpawnChessman(4, 6, 0, orientationLight);
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, i, 1, orientationLight);
        }

        // Dark Team
        // King
        SpawnChessman(6, 3, 7, orientationDark);
        // Queen
        SpawnChessman(7, 4, 7, orientationDark);
        // rooks
        SpawnChessman(8, 0, 7, orientationDark);
        SpawnChessman(8, 7, 7, orientationDark);
        // bishops
        SpawnChessman(9, 2, 7, orientationDark);
        SpawnChessman(9, 5, 7, orientationDark);
        // Knights
        SpawnChessman(10, 1, 7, orientationDark);
        SpawnChessman(10, 6, 7, orientationDark);
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6, orientationDark);
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

    private void EndGame()
    {
        if (isLightTurn)
        {
            Debug.Log("Light team wins");
        }
        else
        {
            Debug.Log("Dark team wins");
        }

        foreach (GameObject go in activeChessman)
        {
            Destroy(go);
        }

        isLightTurn = true;
        BoardHighlights.Instance.HideHighlights();
        SpawnAllChessmans();
    }
}
