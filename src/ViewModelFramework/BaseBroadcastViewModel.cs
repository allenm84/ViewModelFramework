using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseBroadcastViewModel : BaseViewModel
  {
    private readonly ViewModelBroadcaster mBroadcaster;

    public BaseBroadcastViewModel()
    {
      mBroadcaster = new ViewModelBroadcaster(this);
    }

    public void Add(IViewModelReceiver receiver)
    {
      mBroadcaster.Add(receiver);
    }

    public bool Remove(IViewModelReceiver receiver)
    {
      return mBroadcaster.Remove(receiver);
    }

    internal bool Send(BaseViewModel viewModel)
    {
      return mBroadcaster.Send(viewModel);
    }
  }
}
