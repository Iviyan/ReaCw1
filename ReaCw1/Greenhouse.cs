using System.Collections;
using ReaCw1.Plants;

namespace ReaCw1;

public class Greenhouse : IEnumerable<GreenhousePlant>
{
    private Plant?[] _plants;
    private int _available;

    public Greenhouse(int placeCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(placeCount);

        _plants = new Plant[placeCount];
        _available = placeCount;
    }

    public int Capacity => _plants.Length;
    public int Available => _available;
    public int Used => Capacity - Available;

    public Plant? this[int place]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfNegative(place);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(place, Capacity);

            return _plants[place];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(place);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(place, Capacity);

            if (_plants[place] == null && value != null) _available--;
            else if (_plants[place] != null && value == null) _available++;
            
            _plants[place] = value;
        }
    }

    public void Expand(int newPlaceCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(newPlaceCount, Capacity);

        Plant[] newPlants = new Plant[newPlaceCount];
        Array.Copy(_plants, newPlants, _plants.Length);
        _plants = newPlants;
    }

    public IEnumerator<GreenhousePlant> GetEnumerator() =>
        _plants
            .Select((plant, place) => new GreenhousePlant(place, plant!))
            .Where(p => p.Plant != null!)
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record struct GreenhousePlant(int Place, Plant Plant);