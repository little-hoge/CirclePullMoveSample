using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullMoveManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    /// <summary> 描く線の追加予定位置表示用コンポーネント </summary>
    private LineRenderer lineGhostRenderer;

    /// <summary> 描く線の太さ </summary>
    [Range(0, 10)] public float lineWidth;

    /// <summary> ドラッグの開始位置 </summary>
    [SerializeField]
    private Vector3 mouseStartPos;

    /// <summary>　ドラッグの終了位置 </summary>
    [SerializeField]
    private Vector3 mouseEndPos;

    /// <summary> 線作成キャンセルフラグ </summary>
    private bool cancelFlg = false;

    /// <summary> 物理演算コンポーネント </summary>
    [SerializeField]
    Rigidbody2D rigid2d;

    void Start() {
        // コンポーネントを取得する
        this.rigid2d = GetComponent<Rigidbody2D>();
        this.lineGhostRenderer = GetComponent<LineRenderer>();

        // 線の幅を決める
        this.lineGhostRenderer.startWidth = lineWidth;
        this.lineGhostRenderer.endWidth = lineWidth;

        // 頂点の数を決める
        this.lineGhostRenderer.positionCount = 2;

    }

    void Update() {

        // キャンセル状態
        if (Input.GetMouseButtonDown(1) || Input.touchCount > 1) {
            cancelFlg = true;
        }

        // キャンセルフラグが立っている時キャンセル
        if (cancelFlg) {
            this.LinePositionInit();
        }
        // キャンセル解除
        if (Input.GetMouseButtonDown(0) && cancelFlg) {
            cancelFlg = false;
        }
    }

    void FixedUpdate() {
        // 減速
        this.rigid2d.velocity *= 0.995f;

    }

    // ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData) {
        LinePositionInit();
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData) {
        LineEndPositionUpdate();
    }

    // ドラッグ終了
    public void OnEndDrag(PointerEventData eventData) {
        Vector2 startDirection = -1 * (mouseEndPos - mouseStartPos).normalized;
        var moveSpeed = (mouseStartPos - mouseEndPos).magnitude;

        this.rigid2d.AddForce(startDirection * moveSpeed);

        LinePositionInit();

    }

    /// <summary>
    /// 描く線の終了位置情報の更新
    /// </summary>
    public void LineEndPositionUpdate() {
        mouseEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        var endLinePos = Camera.main.ScreenToWorldPoint(mouseEndPos);

        // 追加した頂点の座標を設定
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 2, endLinePos);

    }

    /// <summary>
    /// 描く線の表示位置の初期化
    /// </summary>
    public void LinePositionInit() {
        mouseStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        var startLinePos = Camera.main.ScreenToWorldPoint(mouseStartPos);

        // 追加した頂点の座標を設定
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 1, startLinePos);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 2, startLinePos);

    }
}
