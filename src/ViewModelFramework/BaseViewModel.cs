using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  public abstract class BaseViewModel : BaseDataViewModel
  {
    private Dictionary<string, object> mCommittedValues;

    protected override void InternalDoCancel()
    {
      base.InternalDoCancel();
      Rollback();
    }

    protected void Rollback()
    {
      Push(mCommittedValues);
      InternalRollback();
    }

    protected virtual void InternalRollback()
    {
    }

    protected override void InternalDoAccept()
    {
      base.InternalDoAccept();
      Commit();
    }

    internal void Commit()
    {
      mCommittedValues = Pull().ToDictionary(k => k.Key, v => Clone.Do(v.Value));
      InternalCommit();
    }

    protected virtual void InternalCommit()
    {
    }
  }
}
