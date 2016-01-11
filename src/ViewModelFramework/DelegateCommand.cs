using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelFramework
{
  internal class DelegateCommand : ICommand
  {
    private readonly Action execute;
    private readonly Func<bool> canExecute;

    public DelegateCommand(Action execute)
      : this(execute, null)
    {

    }

    public DelegateCommand(Action execute, Func<bool> canExecute)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public void Refresh()
    {
      var changed = CanExecuteChanged;
      if (changed != null)
      {
        changed(this, EventArgs.Empty);
      }
    }

    public bool CanExecute(object parameter)
    {
      if (canExecute == null)
      {
        return true;
      }

      return canExecute();
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      execute();
    }
  }
}
