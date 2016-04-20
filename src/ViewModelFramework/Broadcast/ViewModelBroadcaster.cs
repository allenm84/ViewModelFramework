using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public sealed class ViewModelBroadcaster
  {
    static readonly Lazy<ViewModelBroadcaster> sLazyInstance;
    static ViewModelBroadcaster()
    {
      sLazyInstance = new Lazy<ViewModelBroadcaster>(() => new ViewModelBroadcaster(), true);
    }

    public static ViewModelBroadcaster Instance
    {
      get { return sLazyInstance.Value; }
    }

    private readonly List<WeakReference<IViewModelReceiver>> mReceivers = new List<WeakReference<IViewModelReceiver>>();

    private ViewModelBroadcaster() { }

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
