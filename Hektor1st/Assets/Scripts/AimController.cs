using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;


    private Camera mainCamera;
    public GameObject _projectilePrefab;
    public GameObject _projectileBombPrefab;
    public Transform _hole;


    private void Start()
    {
        // Cache the camera, Camera.main is an expensive operation.
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim();
            
        if (Input.GetMouseButton(0))
        {
            GameObject _projectile = Instantiate(_projectilePrefab, _hole.position, _hole.rotation);
            _projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 2000));
            Destroy(_projectile, 1.0f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject _projectile2 = Instantiate(_projectileBombPrefab, _hole.position, _hole.rotation);
            _projectile2.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 750));
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
