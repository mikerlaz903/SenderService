using SenderService.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Collections
{
    internal class AddableList<T> : List<T>
    {
        public event EventHandler OnAdd;
        public event EventHandler OnRemove;

        public new void Add(T item)
        {
            var args = new AddableListArgs<T> { NewValue = item };

            OnAdd?.Invoke(this, args);
            base.Add(item);
        }
        public void Add(T item, string user)
        {
            OnAdd?.Invoke(this, null);
            base.Add(item);
        }

        public new bool Remove(T item)
        {
            var args = new AddableListArgs<T> { NewValue = item };

            OnRemove?.Invoke(this, args);
            return base.Remove(item);
        }
    }
}
