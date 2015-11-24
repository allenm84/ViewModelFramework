using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public interface IViewModelReceiver
  {
    bool Receive(BaseViewModel viewModel);
  }
}
