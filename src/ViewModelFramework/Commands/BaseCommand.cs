using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelFramework
{
  public abstract class BaseCommand : ICommand
  {
    public void Refresh()
    {
      var canExecuteChanged = CanExecuteChanged;
      if (canExecuteChanged != null)
      {
        canExecuteChanged(this, EventArgs.Empty);
      }
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return InternalCanExecute(parameter);
    }

    public void Execute(object parameter)
    {
      Invoke(parameter);
    }

    public bool Invoke(object parameter)
    {
      if (!CanExecute(parameter))
      {
        return false;
      }

      InternalExecute(parameter);
      return true;
    }

    protected abstract bool InternalCanExecute(object parameter);

    protected abstract void InternalExecute(object parameter);
  }
}
