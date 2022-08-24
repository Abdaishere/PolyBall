using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineController))]
public class LineController : MonoBehaviour
{
    [SerializeField] private List<Transform> nodes;
    private LineRenderer _lr;

    // Start is called before the first frame update
    private void Start()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = nodes.Count;
    }

    // Update is called once per frame
    private void Update()
    {
        _lr.SetPositions(nodes.ConvertAll(n => n.position - new Vector3(0, 0, 5)).ToArray());   
    }

    public Vector3[] GetPositions() {
        var positions = new Vector3[_lr.positionCount];
        _lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth() {
        return _lr.startWidth;
    }
}
