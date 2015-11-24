using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public sealed class ViewModelBroadcaster
  {
    private readonly List<WeakReference<IViewModelReceiver>> mReceivers = new List<WeakReference<IViewModelReceiver>>();

    public ViewModelBroadcaster(BaseBroadcastViewModel owner)
    {
      Owner = owner;
    }

    public BaseBroadcastViewModel Owner { get; private set; }

    public void Add(IViewModelReceiver receiver)
    {
      mReceivers.Add(new WeakReference<IViewModelReceiver>(receiver));
    }

    public bool Remove(IViewModelReceiver receiver)
    {
      bool removed = false;

      IViewModelReceiver item;
      for (int i = mReceivers.Count - 1; i > -1; --i)
      {
        if (mReceivers[i].TryGetTarget(out item) && ReferenceEquals(item, receiver))
        {
          mReceivers.RemoveAt(i);
          removed = true;
        }
      }

      return removed;
    }

    internal bool Send(BaseViewModel viewModel)
    {
      IViewModelReceiver item;
      foreach (var value in mReceivers)
      {
        if (value.TryGetTarget(out item) && item.Receive(viewModel))
        {
          return true;
        }
      }

      return false;
    }
  }
}
