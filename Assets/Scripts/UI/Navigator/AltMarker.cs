
using PlayerController;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AltMarker : MonoBehaviour
{
    public GameObject marker;
    
    public Material innerMaterial;
    
    private PlayerProfileService _playerProfileService;


    public void Start()
    {
        marker = gameObject.GetNamedChild("Canvas");
    }
    
    
    public void Update()
    {
        marker.transform.LookAt(_playerProfileService.GetPlayerCamera().transform.position);
        
    }

    public void setPlayerProfile(PlayerProfileService profileService)
    {
        _playerProfileService = profileService;
    }
}
