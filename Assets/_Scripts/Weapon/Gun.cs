using System.Collections;
using UnityEngine;

public class Gun :Weapon
{
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private Transform _bulletTrailPrefab;
    [SerializeField] private Transform _shootEffectPrefab;
    [SerializeField] private Transform _hitEffectPrefab;
    [SerializeField] private Transform _decalEffectPrefab;
    public override void ApplyWeapon(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        Transform shootEffect = Instantiate(_shootEffectPrefab, _muzzlePoint.position, _muzzlePoint.rotation);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            Transform trail = Instantiate(_bulletTrailPrefab, _muzzlePoint.position, Quaternion.identity);
            if (hit.transform.gameObject.TryGetComponent<Trigger>(out Trigger trigger))
            {
                StartCoroutine(ShootAnimation(trail.GetComponent<TrailRenderer>(), hit, false));
                trigger.TriggerEnter("shoot", _damage);
            }
            else
            {
                StartCoroutine(ShootAnimation(trail.GetComponent<TrailRenderer>(), hit, true));
            }
        }
        
    }

    private IEnumerator ShootAnimation(TrailRenderer trail, RaycastHit hit, bool withDecal)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        Instantiate(_hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        if(withDecal) Instantiate(_decalEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(trail.gameObject, trail.time);
    }
}
