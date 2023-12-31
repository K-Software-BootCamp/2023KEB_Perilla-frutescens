using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
public enum DoorPortal
{
    Normal,
    Golden,
    Shop,
    Boss
}
public enum DoorType
{
    Up,
    Down,
    Right,
    Left
}

public class Door : MonoBehaviour
{
    public static Door instance;
    public LayerMask whatIsTarget;

    //public bool isCleared = true; 게임매니저에 있는 isCleared 받아옴
    public DoorType doorType;

    public RoomType currentRoomType;
    public RoomType connectRoomType;

    private const float distance = 8.5f;
    private Vector3 doorDirection;

    private const float RayDistance = 10f;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if(doorType == DoorType.Right)
            doorDirection = Vector3.right;
        else if(doorType == DoorType.Left)
            doorDirection = Vector3.left;
        else if(doorType == DoorType.Up)
            doorDirection = Vector3.forward;
        else
            doorDirection = Vector3.back;

        currentRoomType = transform.GetComponentInParent<Room>().roomType;
    }
    public void MoveToRoom(Vector3 direction)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        var tranPosition = playerTransform.position + direction + (Vector3.up * 0.5f);
        playerTransform.position = tranPosition; 
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && GameManager.Instance.IsRoomCleared()) // && isCleared
        {
            Vector3 normalVector;

            switch (doorType)
            {
                case DoorType.Up:
                    normalVector = new Vector3(0, 0, distance);
                    MoveToRoom(normalVector);
                    break;
                case DoorType.Down:
                    normalVector = new Vector3(0, 0, -distance);
                    MoveToRoom(normalVector);
                    break;
                case DoorType.Right:
                    normalVector = new Vector3(distance, 0, 0);
                    MoveToRoom(normalVector);
                    break;
                case DoorType.Left:
                    normalVector = new Vector3(-distance, 0, 0);
                    MoveToRoom(normalVector);
                    break;
                default:
                    break;
            }
            if(connectRoomType == RoomType.Normal) EnemySpawner.Instance.SelectEnemySpawner();
            if (connectRoomType == RoomType.Golden) Room.instance.LookPlayer();
            MinimapCameraFollow.Instance.FollowMinimap();
        }
    }
    public void ShootRay()
    {
        Collider[] hitInfo = new Collider[10];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, 5f, hitInfo, whatIsTarget, QueryTriggerInteraction.Ignore);

        if (numColliders <= 1)
        {
            gameObject.SetActive(false);
        }

        
    }
    public void ChangeImage()
    {
        switch(connectRoomType)
        {
            case RoomType.Normal: //Blue
                Instantiate(MapGenerator.Instance.portals[0], transform);
                break;
            case RoomType.Golden: //Gold
                Instantiate(MapGenerator.Instance.portals[1], transform);
                break;
            case RoomType.Shop: //Green
                Instantiate(MapGenerator.Instance.portals[2], transform);
                break;
            case RoomType.Boss: //Red
                Instantiate(MapGenerator.Instance.portals[3], transform);
                break;
        }
    }
}
