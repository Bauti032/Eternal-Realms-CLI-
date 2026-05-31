using System.Collections.Generic;
using System.Linq;
using EternalRealms.Core.Exceptions;

namespace EternalRealms.Core.Entities
{
    public sealed class Inventory
    {
        private readonly List<Item> _items;

        public Inventory(int capacity)
        {
            Capacity = capacity;
            _items = new List<Item>();
        }

        public int Capacity { get; init; }
        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();
        public bool IsFull => _items.Count >= Capacity;

        public void AddItem(Item item)
        {
            if (IsFull)
            {
                throw new InventoryFullException();
            }

            _items.Add(item);
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);

            if (item is null)
            {
                throw new ItemNotFoundException(itemId.ToString());
            }

            _items.Remove(item);
        }

        public T GetItem<T>(Guid itemId) where T : Item
        {
            var item = _items.OfType<T>().FirstOrDefault(i => i.Id == itemId);

            if (item is null)
            {
                throw new ItemNotFoundException(itemId.ToString());
            }

            return item;
        }

        public bool Contains(Guid itemId) => _items.Any(item => item.Id == itemId);
    }
}

