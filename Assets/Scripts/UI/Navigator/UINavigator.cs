using System.Collections;
using System.Collections.Generic;
using Game.Tasks;
using UnityEngine;

public class UINavigator : MonoBehaviour
{
    private List<ObjectiveMarker> markers = new List<ObjectiveMarker>();
    private RectTransform compassBarTransform;

    public HUD parentHUD;
    public GameObject markerPrefab;

    public float zThreshold;

    // Start is called before the first frame update
    void Start()
    {
        compassBarTransform = gameObject.GetComponent<RectTransform>();


        InitializeMarker(null, new Vector3(10, 10, 10));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateMarkers(Transform cameraTransform)
    {
        Camera cam = parentHUD.ParentCamera();
        foreach (var marker in markers)
        {
            UpdateMarkerPosition(marker, cam);
            CheckPointer(marker, cameraTransform.position);
        }
    }

    void UpdateMarkerPosition(ObjectiveMarker marker, Camera cam)
    {
        Transform cameraTransform = cam.transform;
        
        RectTransform markerTransform = marker.GetCanvas().GetComponent<RectTransform>();
        
        
        Vector3 direction = marker.GetTaskLocation() - cameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(direction.x, direction.z),
            new Vector2(cameraTransform.transform.forward.x, cameraTransform.transform.forward.z));

        float compassPositionX = Mathf.Clamp(2 * angle / cam.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, transform.position.y);
    }

    void CheckPointer(ObjectiveMarker marker, Vector3 playerLocation)
    {
        if (marker.GetTaskLocation().z > playerLocation.z + zThreshold)
        {
            marker.EnablePointer(marker.pointerUp);
        }
        else
        {
            marker.DisablePointer(marker.pointerUp);
        }

        if (marker.GetTaskLocation().z > playerLocation.z - zThreshold)
        {
            marker.EnablePointer(marker.pointerDown);
        }
        else
        {
            marker.DisablePointer(marker.pointerDown);
        }
    }


    public void InitializeMarker(GameTask task, Vector3 location)
    {
        GameObject marker = Instantiate(markerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        marker.transform.position = new Vector3(transform.position.x,0,0);
        ObjectiveMarker newMarker = marker.GetComponent<ObjectiveMarker>();
        
        // create new marker and set as child of this object
        
        newMarker.transform.SetParent(gameObject.transform);
        
        newMarker.Initialize(task, location);
        
        markers.Add(newMarker);
    }

    public void DismissMarker(GameTask task)
    {
        ObjectiveMarker markerToRemove = null;
        foreach (var marker in markers)
        {
            if (marker.GetTask() == task)
            {
                markerToRemove = marker;
                break;
            }
        }

        if (markerToRemove == null)
        {
            Debug.Log("This marker was not registered!");
            return;
        }

        markers.Remove(markerToRemove);
    }
    
    
}