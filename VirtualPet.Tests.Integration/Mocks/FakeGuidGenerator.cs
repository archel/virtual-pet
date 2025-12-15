using VirtualPet.Domain.Pet;

namespace VirtualPet.Tests.Integration.Mocks;

public class FakeGuidGenerator : IGuidGenerator
{
    private readonly Queue<Guid> _guids = new();

    public void EnqueueGuid(Guid guid)
    {
        _guids.Enqueue(guid);
    }

    public Guid NewGuid()
    {
        return _guids.Count == 0 ? throw new InvalidOperationException("No GUIDs available in the fake generator.") : _guids.Dequeue();
    }
}