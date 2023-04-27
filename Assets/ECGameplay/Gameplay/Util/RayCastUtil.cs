using UnityEngine;

namespace ECGameplay
{
    public static class RayCastUtil
    {
        public static bool CastMapPoint(out Vector3 hitPoint)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, 1 << LayerMask.NameToLayer("Ground")))
            {
                hitPoint = new Vector3(hit.point.x, 0, hit.point.z);
                return true;
            }
            hitPoint = Vector3.zero;
            return false;
        }
    }
}