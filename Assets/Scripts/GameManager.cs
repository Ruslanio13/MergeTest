using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Humanoid> _selectedHumanoids = new List<Humanoid>();
    [SerializeField] private LineRenderer _selectedHumanoidsLine;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Input.touchCount == 0 && _selectedHumanoids.Count > 1)
            FinishMerge();
    }

    public void AddHumanoid(Humanoid humanoid)
    {
        if ((_selectedHumanoids.Count == 0 || _selectedHumanoids[0].GetHealth() == humanoid.GetHealth()) && (!_selectedHumanoids.Contains(humanoid)))
        {
            _selectedHumanoids.Add(humanoid);
            _selectedHumanoidsLine.positionCount++;
            _selectedHumanoidsLine.SetPosition(_selectedHumanoids.Count - 1, humanoid.transform.position);
        }
    }

    private void FinishMerge()
    {
        int count = _selectedHumanoids.Count;
        _selectedHumanoids[count - 1].UpgradeHumanoid(count);
        for (int i = 0; i < count - 1; i++)
            Destroy(_selectedHumanoids[i].gameObject);

        _selectedHumanoids.Clear();
        _selectedHumanoidsLine.positionCount = 0;
    }
}