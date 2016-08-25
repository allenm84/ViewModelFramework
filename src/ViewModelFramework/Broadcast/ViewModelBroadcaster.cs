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

    private readonly List<IViewModelReceiver> mReceivers = new List<IViewModelReceiver>();

    private ViewModelBroadcaster() { }

    public void Add(IViewModelReceiver receiver)
    {
      mReceivers.Add(receiver);
    }

    public bool Remove(IViewModelReceiver receiver)
    {
      return mReceivers.Remove(receiver);
    }

    internal bool Send(BaseViewModel viewModel)
    {
      foreach (var receiver in mReceivers)
      {
        if (receiver.Receive(viewModel))
        {
          return true;
        }
      }

      return false;
    }
  }
}
