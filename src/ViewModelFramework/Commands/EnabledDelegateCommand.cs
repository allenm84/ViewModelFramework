using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public class EnabledDelegateCommand : DelegateCommand
  {
    private bool mEnabled;

    public EnabledDelegateCommand(Action execute, Func<bool> canExecute = null) 
      : base(execute, canExecute)
    {
      mEnabled = true;
    }

    public bool Enabled
    {
      get { return mEnabled; }
      set
      {
        if (mEnabled != value)
        {
          mEnabled = value;
          Refresh();
        }
      }
    }

    /// <summary> Enables the command and then disables it on Dispose. </summary>
    public IDisposable Enable()
    {
      return new EnabledDelegateCommandToggle(this, true);
    }

    /// <summary> Disables the command and then enables it on Dispose. </summary>
    public IDisposable Disable()
    {
      return new EnabledDelegateCommandToggle(this, false);
    }

    protected override bool InternalCanExecute(object parameter)
    {
      return mEnabled && base.InternalCanExecute(parameter);
    }

    private class EnabledDelegateCommandToggle : IDisposable
    {
      private bool mValue;
      private EnabledDelegateCommand mCommand;

      public EnabledDelegateCommandToggle(EnabledDelegateCommand command, bool desired)
      {
        mValue = !desired;
        mCommand = command;
        mCommand.Enabled = desired;
      }

      void IDisposable.Dispose()
      {
        if (mCommand != null)
        {
          mCommand.Enabled = mValue;
          mCommand = null;
        }
      }
    }
  }
}
