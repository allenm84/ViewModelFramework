using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelFramework
{
  /// <summary>
  /// Read-only wrapper around a BindingList.
  /// </summary>
  public class ReadOnlyBindingList<T> : ReadOnlyCollection<T>, IBindingList, ICancelAddNew, IRaiseItemChangedEvents
  {
    private BindingList<T> mList;

    /// <summary>
    /// Initializes a new instance of ReadOnlyBindingList that wraps the given BindingList.
    /// </summary>
    public ReadOnlyBindingList(BindingList<T> list) 
      : base(list)
    {
      mList = list;
      mList.ListChanged += mList_ListChanged;
    }

    private void mList_ListChanged(object sender, ListChangedEventArgs e)
    {
      FireListChanged(e);
    }

    private void FireListChanged(ListChangedEventArgs e)
    {
      var changed = ListChanged;
      if (changed != null)
      {
        changed(this, e);
      }
    }

    /// <summary> Gets/sets a value indicating if ListChanged events are raised. </summary>
    public bool RaiseListChangedEvents
    {
      get { return mList.RaiseListChangedEvents; }
      set { mList.RaiseListChangedEvents = value; }
    }

    /// <summary> Always false. </summary>
    public bool AllowEdit { get { return false; } }

    /// <summary> Always false. </summary>
    public bool AllowNew { get { return false; } }

    /// <summary> Always false. </summary>
    public bool AllowRemove { get { return false; } }

    /// <summary> Gets whether the items in the list are sorted. </summary>
    public bool IsSorted
    {
      get { return ((IBindingList)this.mList).IsSorted; }
    }

    /// <summary> Gets a value indicating whether or not ListChanged events are raised. </summary>
    public bool RaisesItemChangedEvents
    {
      get { return ((IRaiseItemChangedEvents)this.mList).RaisesItemChangedEvents; }
    }

    /// <summary> Gets the direction of the sort. </summary>
    public ListSortDirection SortDirection
    {
      get { return ((IBindingList)this.mList).SortDirection; }
    }

    /// <summary> Gets the property which is being used for sort. </summary>
    public PropertyDescriptor SortProperty
    {
      get { return ((IBindingList)this.mList).SortProperty; }
    }

    /// <summary> Gets whether or not ListChanged events are raised when the list or an item changes. </summary>
    public bool SupportsChangeNotification
    {
      get { return ((IBindingList)this.mList).SupportsChangeNotification; }
    }

    /// <summary> Gets a value indicating if search is supported using the Find method. </summary>
    public bool SupportsSearching
    {
      get { return ((IBindingList)this.mList).SupportsSearching; }
    }

    /// <summary> Gets whether the list supports sorting. </summary>
    public bool SupportsSorting
    {
      get { return ((IBindingList)this.mList).SupportsSorting; }
    }

    /// <summary> Event raised when the list or an item is changed. </summary>
    public event ListChangedEventHandler ListChanged;

    /// <summary> Adds the property used for searching. </summary>
    public void AddIndex(PropertyDescriptor property)
    {
      ((IBindingList)this.mList).AddIndex(property);
    }

    /// <summary> Not supported. </summary>
    public object AddNew()
    {
      throw new NotSupportedException();
    }

    /// <summary> Sorts the list based on the property in the specified direction. </summary>
    public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
    {
      ((IBindingList)this.mList).ApplySort(property, direction);
    }

    /// <summary> Not supported. </summary>
    public void CancelNew(int itemIndex)
    {
      throw new NotSupportedException();
    }

    /// <summary> Not supported. </summary>
    public void EndNew(int itemIndex)
    {
      throw new NotSupportedException();
    }

    /// <summary> Returns the index of the row that has the specified property. </summary>
    public int Find(PropertyDescriptor property, object key)
    {
      return ((IBindingList)this.mList).Find(property, key);
    }

    /// <summary> Removes the property used for searching. </summary>
    public void RemoveIndex(PropertyDescriptor property)
    {
      ((IBindingList)this.mList).RemoveIndex(property);
    }

    /// <summary> Removes any applied sort. </summary>
    public void RemoveSort()
    {
      ((IBindingList)this.mList).RemoveSort();
    }
  }
}
