using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;
    [SerializeField] Material material;

    public Camera myCamera;

    public Transform renderSurface;
    public Transform portalCollider;

    private GameObject player;
    private PortalTeleport portalTeleport;

    private PortalCamera portalCamera;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        portalTeleport = portalCollider.GetComponent<PortalTeleport>();
        portalTeleport.player = player.transform;
        portalTeleport.receiver = otherPortal.portalCollider;

        renderSurface.GetComponent<Renderer>().material = Instantiate(material);
        if (myCamera.targetTexture != null) {
            myCamera.targetTexture.Release();
        }
        myCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
    }

    private void Start () {
        renderSurface.GetComponent<Renderer>().material.mainTexture = otherPortal.GetComponent<Portal>().myCamera.targetTexture;
    }
}
