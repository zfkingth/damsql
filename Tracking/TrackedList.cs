using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace hammergo.Tracking
{
    public class TrackedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
         where T : ITrackable, INotifyPropertyChanged
    {
        List<T> list = null;
        List<T> deletedList = new List<T>();

        // Listen for changes to each item
        private bool tracking=false;
        public bool Tracking
        {
            get { return tracking; }
            set
            {
                foreach (T item in list)
                {
                    if (value) item.PropertyChanged += OnItemChanged;
                    else item.PropertyChanged -= OnItemChanged;
                }
                tracking = value;
            }
        }


        void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Tracking) return;
            ITrackable item = (ITrackable)sender;
            if (e.PropertyName != "TrackingState")
            {
                // Mark item as updated
                if (item.TrackingState == TrackingInfo.Unchanged)
                    item.TrackingState = TrackingInfo.Updated;
            }
        }

        /// <summary>
        /// 处理插入时的状态
        /// </summary>
        /// <param name="item"></param>
        void itemAdded(T item)
        {
            if (Tracking)
            {
                item.TrackingState = TrackingInfo.Created;
                item.PropertyChanged += OnItemChanged;
            }
        }

        void itemAdded(IEnumerable<T> collection)
        {
            if (Tracking)
            {
                IEnumerator<T> iterator = collection.GetEnumerator();
                while (iterator.MoveNext())
                {
                    itemAdded(iterator.Current);
                }
            }
        }

        /// <summary>
        /// 处理删除后的状态
        /// </summary>
        /// <param name="item"></param>
        void itemRemoved(T item)
        {
            if (Tracking)
            {
                if (item.TrackingState != TrackingInfo.Created)
                {
                    item.PropertyChanged -= OnItemChanged;
                    item.TrackingState = TrackingInfo.Deleted;
                    deletedList.Add(item);
                }
            }
        }


        void itemRemoved(IEnumerable<T> collection)
        {
            if (Tracking)
            {
                IEnumerator<T> iterator = collection.GetEnumerator();
                while (iterator.MoveNext())
                {
                    itemRemoved(iterator.Current);
                }
            }
        }

        public List<T> GetChanges()
        {
            // Create a collection with changed items
            List<T> changes = new List<T>();
            foreach (T existing in this.list)
            {
                if (existing.TrackingState != TrackingInfo.Unchanged)
                    changes.Add(existing);
            }

            // Append deleted items
            foreach (T deleted in deletedList)
                changes.Add(deleted);
            return changes;
        }

        public List<T> GetDeleted()
        {
            // Create a collection with changed items
            List<T> changes = new List<T>(deletedList);
          

          
            return changes;
        }

        public List<T> GetUpdated()
        {
            // Create a collection with changed items
            List<T> changes = new List<T>();
            foreach (T existing in list)
            {
                if (existing.TrackingState == TrackingInfo.Updated)
                    changes.Add(existing);
            }


            return changes;
        }

        public List<T> GetCreated()
        {
            // Create a collection with changed items
            List<T> changes = new List<T>();
            foreach (T existing in list)
            {
                if (existing.TrackingState == TrackingInfo.Created)
                    changes.Add(existing);
            }


            return changes;
        }

        

        #region IList<T> 成员

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
           
            list.Insert(index, item);
            itemAdded(item);
        }

        public void RemoveAt(int index)
        {
            if (Tracking)
            {
                T item = list[index];
                itemRemoved(item);
            }
            list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<T> 成员

        public void Add(T item)
        {
            list.Add(item);
            itemAdded(item);
        }

        /// <summary>
        /// 从 List 中移除所有元素
        /// </summary>
        public void Clear()
        {
            itemRemoved(list);
            list.Clear();
        }

        /// <summary>
        /// 确定某元素是否在 List 中
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// 已重载。 将 List 或它的一部分复制到一个数组中
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }


        /// <summary>
        /// 获取 List 中实际包含的元素数
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get
            {
                ICollection<T> icl= list as ICollection<T>;
                return icl.IsReadOnly;
            
            }
        }

        /// <summary>
        /// 从 List 中移除特定对象的第一个匹配项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            bool ret= list.Remove(item);
            itemRemoved(item);
            return ret;
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable ie = list as IEnumerable;
            return ie.GetEnumerator();
        }

        #endregion

        #region IList 成员

        int IList.Add(object value)
        {
            IList il = list as IList;
            int ret= il.Add(value);
            itemAdded((T)value);
            return ret;
        }

        void IList.Clear()
        {

            this.Clear();
        }

        bool IList.Contains(object value)
        {
            IList il = list as IList;
            return il.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            IList il = list as IList;
            return il.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            IList il = list as IList;
            il.Insert(index, value);
            itemAdded((T)value);
        }

        bool IList.IsFixedSize
        {
            get { IList il = list as IList; 
                return il.IsFixedSize; }
        }

        bool IList.IsReadOnly
        {
            get { IList il = list as IList; 
                return il.IsReadOnly; }
        }

        void IList.Remove(object value)
        {
           

            this.Remove((T)value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
               return list[index];
            }
            set
            {
                IList il = list as IList;
                il[index] = value;
            }
        }

        #endregion

        #region ICollection 成员

        void ICollection.CopyTo(Array array, int index)
        {
            ICollection ic = list as ICollection;
            ic.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return list.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { ICollection ic = list as ICollection; 
                    return ic.IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get {
                ICollection ic = list as ICollection; 
                return ic.SyncRoot;
            }
        }

        #endregion

        #region list的其它成员

        /// <summary>
        /// 将指定集合的元素添加到 List 的末尾。 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection);
            itemAdded(collection);
        }

        /// <summary>
        /// 返回当前集合的只读 IList 包装。 
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return list.AsReadOnly();
        }

        /// <summary>
        /// 使用默认的比较器在整个已排序的 List 中搜索元素，并返回该元素从零开始的索引。 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int BinarySearch(T item)
        {
            return list.BinarySearch(item);
        }

        /// <summary>
        /// 使用指定的比较器在整个已排序的 List 中搜索元素，并返回该元素从零开始的索引。 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return list.BinarySearch(item, comparer);
        }

        /// <summary>
        /// 使用指定的比较器在已排序 List 的某个元素范围中搜索元素，并返回该元素从零开始的索引。 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return list.BinarySearch(index, count, item, comparer);
        }


        /// <summary>
        /// 将当前 List 中的元素转换为另一种类型，并返回包含转换后的元素的列表。
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="converter"></param>
        /// <returns></returns>
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return list.ConvertAll<TOutput>(converter);
        }

        /// <summary>
        /// 将整个 List 复制到兼容的一维数组中，从目标数组的开头开始放置。 
        /// </summary>
        /// <param name="array"></param>
        public void CopyTo(T[] array)
        {
            list.CopyTo(array);
        }

        /// <summary>
        /// 将一定范围的元素从 List 复制到兼容的一维数组中，从目标数组的指定索引位置开始放置
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <param name="count"></param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            list.CopyTo(index, array, arrayIndex, count);
        }


        /// <summary>
        /// 确定 List 是否包含与指定谓词所定义的条件相匹配的元素
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool Exists(Predicate<T> match)
        {
            return list.Exists(match);
        }


        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 List 中的第一个匹配元素
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T Find(Predicate<T> match)
        {
            return list.Find(match);
        }


        /// <summary>
        /// 检索与指定谓词所定义的条件相匹配的所有元素
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public List<T> FindAll(Predicate<T> match)
        {
            return list.FindAll(match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 List 中第一个匹配元素的从零开始的索引
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindIndex(Predicate<T> match)
        {
            return list.FindIndex(match);
        }


        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回 List 中从指定索引到最后一个元素的元素范围内第一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return list.FindIndex(startIndex, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的一个元素，并返回 List 中从指定的索引开始、包含指定元素个数的元素范围内第一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return list.FindIndex(startIndex, count, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 List 中的最后一个匹配元素
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T FindLast(Predicate<T> match)
        {
            return list.FindLast(match);
        }


        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 List 中最后一个匹配元素的从零开始的索引
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindLastIndex(Predicate<T> match)
        {
            return list.FindLastIndex(match);
        }


        /// <summary>
        /// 搜索与由指定谓词定义的条件相匹配的元素，并返回 List 中从第一个元素到指定索引的元素范围内最后一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return list.FindLastIndex(startIndex, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回 List 中包含指定元素个数、到指定索引结束的元素范围内最后一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return list.FindLastIndex(startIndex, count, match);
        }


        /// <summary>
        /// 对 List 的每个元素执行指定操作
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T> action)
        {
            list.ForEach(action);
        }


        /// <summary>
        /// 创建源 List 中的元素范围的浅表副本
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> GetRange(int index, int count)
        {
            return list.GetRange(index, count);
        }


     


        /// <summary>
        /// 搜索指定的对象，并返回 List 中从指定索引到最后一个元素的元素范围内第一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index)
        {
            return list.IndexOf(item, index);
        }


        /// <summary>
        /// 搜索指定的对象，并返回 List 中从指定的索引开始并包含指定的元素数的元素范围内第一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index, int count)
        {
            return list.IndexOf(item, index, count);
        }


        /// <summary>
        /// 将集合中的某个元素插入 List 的指定索引处
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
           
            list.InsertRange(index, collection);
            itemAdded(collection);
        }


        /// <summary>
        /// 移除与指定的谓词所定义的条件相匹配的所有元素
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int RemoveAll(Predicate<T> match)
        {
            if(Tracking)
            itemRemoved(list.FindAll(match));

           return list.RemoveAll(match);
        }


        /// <summary>
        /// 从 List 中移除一定范围的元素
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void RemoveRange(int index, int count)
        {
            if (Tracking)
            {
                for (int i = 0; i < count; i++)
                {
                    itemRemoved(list[i]);
                }
            }
            list.RemoveRange(index, count);
        }

        /// <summary>
        /// 将整个 List 中元素的顺序反转
        /// </summary>
        public void Reverse()
        {
            list.Reverse();
        }

        /// <summary>
        /// 将指定范围中元素的顺序反转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void Reverse(int index, int count)
        {
            list.Reverse(index, count);
        }


        /// <summary>
        /// 使用默认比较器对整个 List 中的元素进行排序
        /// </summary>
        public void Sort()
        {
            list.Sort();
        }

        /// <summary>
        /// 使用指定的 System.Comparison 对整个 List 中的元素进行排序
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<T> comparison)
        {
            list.Sort(comparison);
        }

        /// <summary>
        /// 用指定的比较器对整个 List 中的元素进行排序
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<T> comparer)
        {
            list.Sort(comparer);
        }


        /// <summary>
        /// 使用指定的比较器对 List 中某个范围内的元素进行排序
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            list.Sort(index, count, comparer);
        }

        /// <summary>
        /// 将 List 的元素复制到新数组中
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return list.ToArray();
        }

        /// <summary>
        /// 将容量设置为 List 中的实际元素数目（如果该数目小于某个阈值）
        /// </summary>
        public void TrimExcess()
        {
            list.TrimExcess();
        }


        /// <summary>
        /// 确定是否 List 中的每个元素都与指定的谓词所定义的条件相匹配
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool TrueForAll(Predicate<T> match)
        {
            return list.TrueForAll(match);
        }


        /// <summary>
        /// 获取或设置该内部数据结构在不调整大小的情况下能够保存的元素总数
        /// </summary>
        public int Capacity 
        { 
            get
            {
                return list.Capacity;
            }
            set
            {
                list.Capacity = value;
            }
        }


        /// <summary>
        /// 初始化 List 类的新实例，该实例为空并且具有默认初始容量
        /// </summary>
        public TrackedList()
        {
            list = new List<T>();
        }

        /// <summary>
        /// 初始化 List 类的新实例，该实例包含从指定集合复制的元素并且具有足够的容量来容纳所复制的元素
        /// </summary>
        /// <param name="collection"></param>
        public TrackedList(IEnumerable<T> collection)
        {
            list = new List<T>(collection);
        }

        public TrackedList(int capacity)
        {
            list = new List<T>(capacity);
        }

        #endregion


        public void AcceptChanges()
        {
            deletedList.Clear();

            foreach (T item in this.list)
            {
                item.TrackingState = TrackingInfo.Unchanged;
            }
        }
    }
}
