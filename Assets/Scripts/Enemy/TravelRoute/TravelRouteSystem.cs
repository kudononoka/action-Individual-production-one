using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TravelRouteSystem : MonoBehaviour
{
    /// <summary>移動する対象のTransform</summary>
    Transform _target;

    /// <summary>移動経路地点</summary>
    DestinationPoint[] _destinationPoints;

    /// <summary>周回するかどうか・True : 周回する/false : 周回せずに折り返す</summary>
    [Header("周回するかどうか")]
    [SerializeField] bool _isGoAround = false;

    /// <summary>移動にかかる時間</summary>
    [Header("１回の移動にかかる時間")]
    [SerializeField] float _completeTime = 5;

    /// <summary>移動のためのシーケンス</summary>
    Sequence _moveSequence;

    public void Init(Transform target, MoveDestinationPoint destination)
    {
        _destinationPoints = destination.Point;
        _target = target;
    }

    public void PreparingToMove()
    {
        //移動経路をVector３配列に入れなおす
        Vector3 startPos = _target.TransformPoint(_destinationPoints[0]._point);
        _target.position = startPos;
        Vector3[] point;
        point = new Vector3[_destinationPoints.Length - 1];
        for (int i = 0; i < point.Length; i++)
        {
            point[i] = _target.TransformPoint(_destinationPoints[i + 1]._point);
        }
        _moveSequence = DOTween.Sequence();
        //周回する
        if (_isGoAround)
        {
            _moveSequence.Append(_target.DOPath(point, _completeTime, PathType.CatmullRom) //移動
                　　.SetLoops(-1, LoopType.Restart)　//ループ
                  　.SetOptions(_isGoAround) //移動に関して終点と始点をつなぐ
                    .SetEase(Ease.Linear) //速度一定
                    .SetLookAt(0.01f, _target.forward)); //オブジェクトの正面を操作
        }
        //来た道を折り返す(往復)
        else
        {
            //折り返し用　道順どおりに地点を新しい配列に入れる
            Vector3[] pointback;
            pointback = new Vector3[_destinationPoints.Length - 1];
            for (int i = 0; i < pointback.Length; i++)
            {
                pointback[i] = _target.TransformPoint(_destinationPoints[_destinationPoints.Length - 2 - i]._point);
            }
            _moveSequence.Append(_target.DOPath(point, _completeTime, PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0.01f, _target.forward))　//行き
                    .Append(_target.DOLookAt(point[point.Length - 2], 1f)) //方向転換
                    .Append(_target.DOPath(pointback, _completeTime, PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0.01f, Vector3.forward))　//帰り
                    .Append(_target.DOLookAt(point[0], 1f))　//方向転換
                    .SetLoops(-1, LoopType.Restart);　//ループ
        }
    }

    /// <summary>移動一時停止</summary>
    public void PatrolPause()
    {
        _moveSequence.Pause();
    }

    /// <summary>移動開始</summary>
    public void PatrolPlay()
    {
        _moveSequence.Play();
    }
}