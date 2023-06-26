using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private House _housePrefab;
    [SerializeField] private Commonwealth _commonwealth;
    [SerializeField] private TaxesCollector _taxesCollector;
    [SerializeField] private List<Building> _built;
    [SerializeField] private RoadDrawer _roadDrawer;

    [SerializeField] private int _step;

    [SerializeField] private int MapSize;

    private Building[,] _buildingsMap;

    private void Start()
    {
        Initialize();

        _commonwealth.OnItemGot.AddListener((value) => BuildHouse());
    }

    public void BuildHouse()
    {
        if (!_housePrefab.IsRequirementsMet(_commonwealth))
            return;

        Vector3[] places = GetAvailablePlaces();
        foreach (Vector3 place in places)
        {
            House newHouse = (House)TryToBuild(_housePrefab, place);
            if (newHouse)
            {
                newHouse.GetResources(_commonwealth);
                newHouse.SetCommonwealth(_commonwealth);
                _taxesCollector.AddHouse(newHouse);
                return;
            }
        }
    }
    private void Initialize()
    {
        _buildingsMap = new Building[MapSize, MapSize];

        foreach (Building building in _built)
        {
            Register(building);
        }


        foreach (Building building in _built)
        {
            TryToDrawRoad(building);
        }
    }
    private void Register(Building newBuilding)
    {
        int x = (int)newBuilding.transform.position.x / _step;
        int y = (int)newBuilding.transform.position.z / _step;

        newBuilding.indeces = new Vector2Int(x, y);

        _buildingsMap[x, y] = newBuilding;

        newBuilding.Initialize(_parameters);

        TryToDrawRoad(newBuilding);
    }
    private Building TryToBuild(Building building, Vector3 position)
    {
        Collider[] colliders = Physics.OverlapBox(position, new Vector3(3, 5, 3), Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out HealthObject healthObject))
            {
                return null;
            }
        }
        Building newBuilding = Instantiate(building, position, Quaternion.identity, transform);
        _built.Add(newBuilding);
        Register(newBuilding);
        TryToDrawRoad(newBuilding);

        return newBuilding;
    }
    private Vector3[] GetAvailablePlaces()
    {
        List<Vector3> places = new List<Vector3>();
        foreach (Building building in _built)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Vector2Int delta = new Vector2Int(i, j);
                    Vector2Int currentIndeces = building.indeces + delta;
                    if (!IsValidIndeces(currentIndeces))
                        continue;
                    Building currentBuilding = _buildingsMap[currentIndeces.x, currentIndeces.y];
                    if (delta.magnitude == 1 && !currentBuilding)
                    {
                        places.Add(new Vector3(currentIndeces.x * _step, 0, currentIndeces.y * _step));
                    }
                }
            }
        }
        return places.ToArray();
    }
    private void TryToDrawRoad(Building building)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector2Int delta = new Vector2Int(i, j);
                Vector2Int currentIndeces = building.indeces + delta;

                if (!IsValidIndeces(currentIndeces))
                    continue;

                Building currentBuilding = _buildingsMap[currentIndeces.x, currentIndeces.y];
                if (delta.magnitude == 1 && currentBuilding)
                {
                    if (delta.x != 0)
                        _roadDrawer.DrawHorizontal(Vector3.Lerp(currentBuilding.transform.position, building.transform.position, 0.5f));
                    else
                        _roadDrawer.DrawVertical(Vector3.Lerp(currentBuilding.transform.position, building.transform.position, 0.5f));
                }
            }
        }

    }
    private bool IsValidIndeces(Vector2Int indeces)
    {
        if (indeces.x > 0 && indeces.x < MapSize-1 && indeces.y > 0 && indeces.y < MapSize-1)
            return true;

        return false;
    }
    
}
