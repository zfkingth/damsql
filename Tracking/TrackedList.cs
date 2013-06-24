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
        /// �������ʱ��״̬
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
        /// ����ɾ�����״̬
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

        

        #region IList<T> ��Ա

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

        #region ICollection<T> ��Ա

        public void Add(T item)
        {
            list.Add(item);
            itemAdded(item);
        }

        /// <summary>
        /// �� List ���Ƴ�����Ԫ��
        /// </summary>
        public void Clear()
        {
            itemRemoved(list);
            list.Clear();
        }

        /// <summary>
        /// ȷ��ĳԪ���Ƿ��� List ��
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// �����ء� �� List ������һ���ָ��Ƶ�һ��������
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }


        /// <summary>
        /// ��ȡ List ��ʵ�ʰ�����Ԫ����
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
        /// �� List ���Ƴ��ض�����ĵ�һ��ƥ����
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

        #region IEnumerable<T> ��Ա

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable ��Ա

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable ie = list as IEnumerable;
            return ie.GetEnumerator();
        }

        #endregion

        #region IList ��Ա

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

        #region ICollection ��Ա

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

        #region list��������Ա

        /// <summary>
        /// ��ָ�����ϵ�Ԫ����ӵ� List ��ĩβ�� 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection);
            itemAdded(collection);
        }

        /// <summary>
        /// ���ص�ǰ���ϵ�ֻ�� IList ��װ�� 
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return list.AsReadOnly();
        }

        /// <summary>
        /// ʹ��Ĭ�ϵıȽ���������������� List ������Ԫ�أ������ظ�Ԫ�ش��㿪ʼ�������� 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int BinarySearch(T item)
        {
            return list.BinarySearch(item);
        }

        /// <summary>
        /// ʹ��ָ���ıȽ���������������� List ������Ԫ�أ������ظ�Ԫ�ش��㿪ʼ�������� 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return list.BinarySearch(item, comparer);
        }

        /// <summary>
        /// ʹ��ָ���ıȽ����������� List ��ĳ��Ԫ�ط�Χ������Ԫ�أ������ظ�Ԫ�ش��㿪ʼ�������� 
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
        /// ����ǰ List �е�Ԫ��ת��Ϊ��һ�����ͣ������ذ���ת�����Ԫ�ص��б�
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="converter"></param>
        /// <returns></returns>
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return list.ConvertAll<TOutput>(converter);
        }

        /// <summary>
        /// ������ List ���Ƶ����ݵ�һά�����У���Ŀ������Ŀ�ͷ��ʼ���á� 
        /// </summary>
        /// <param name="array"></param>
        public void CopyTo(T[] array)
        {
            list.CopyTo(array);
        }

        /// <summary>
        /// ��һ����Χ��Ԫ�ش� List ���Ƶ����ݵ�һά�����У���Ŀ�������ָ������λ�ÿ�ʼ����
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
        /// ȷ�� List �Ƿ������ָ��ν���������������ƥ���Ԫ��
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool Exists(Predicate<T> match)
        {
            return list.Exists(match);
        }


        /// <summary>
        /// ������ָ��ν���������������ƥ���Ԫ�أ����������� List �еĵ�һ��ƥ��Ԫ��
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T Find(Predicate<T> match)
        {
            return list.Find(match);
        }


        /// <summary>
        /// ������ָ��ν���������������ƥ�������Ԫ��
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public List<T> FindAll(Predicate<T> match)
        {
            return list.FindAll(match);
        }

        /// <summary>
        /// ������ָ��ν���������������ƥ���Ԫ�أ����������� List �е�һ��ƥ��Ԫ�صĴ��㿪ʼ������
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindIndex(Predicate<T> match)
        {
            return list.FindIndex(match);
        }


        /// <summary>
        /// ������ָ��ν���������������ƥ���Ԫ�أ������� List �д�ָ�����������һ��Ԫ�ص�Ԫ�ط�Χ�ڵ�һ��ƥ����Ĵ��㿪ʼ������
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return list.FindIndex(startIndex, match);
        }

        /// <summary>
        /// ������ָ��ν���������������ƥ���һ��Ԫ�أ������� List �д�ָ����������ʼ������ָ��Ԫ�ظ�����Ԫ�ط�Χ�ڵ�һ��ƥ����Ĵ��㿪ʼ������
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
        /// ������ָ��ν���������������ƥ���Ԫ�أ����������� List �е����һ��ƥ��Ԫ��
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T FindLast(Predicate<T> match)
        {
            return list.FindLast(match);
        }


        /// <summary>
        /// ������ָ��ν���������������ƥ���Ԫ�أ����������� List �����һ��ƥ��Ԫ�صĴ��㿪ʼ������
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindLastIndex(Predicate<T> match)
        {
            return list.FindLastIndex(match);
        }


        /// <summary>
        /// ��������ָ��ν�ʶ����������ƥ���Ԫ�أ������� List �дӵ�һ��Ԫ�ص�ָ��������Ԫ�ط�Χ�����һ��ƥ����Ĵ��㿪ʼ������
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return list.FindLastIndex(startIndex, match);
        }

        /// <summary>
        /// ������ָ��ν���������������ƥ���Ԫ�أ������� List �а���ָ��Ԫ�ظ�������ָ������������Ԫ�ط�Χ�����һ��ƥ����Ĵ��㿪ʼ������
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
        /// �� List ��ÿ��Ԫ��ִ��ָ������
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T> action)
        {
            list.ForEach(action);
        }


        /// <summary>
        /// ����Դ List �е�Ԫ�ط�Χ��ǳ����
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> GetRange(int index, int count)
        {
            return list.GetRange(index, count);
        }


     


        /// <summary>
        /// ����ָ���Ķ��󣬲����� List �д�ָ�����������һ��Ԫ�ص�Ԫ�ط�Χ�ڵ�һ��ƥ����Ĵ��㿪ʼ������
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index)
        {
            return list.IndexOf(item, index);
        }


        /// <summary>
        /// ����ָ���Ķ��󣬲����� List �д�ָ����������ʼ������ָ����Ԫ������Ԫ�ط�Χ�ڵ�һ��ƥ����Ĵ��㿪ʼ������
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
        /// �������е�ĳ��Ԫ�ز��� List ��ָ��������
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
           
            list.InsertRange(index, collection);
            itemAdded(collection);
        }


        /// <summary>
        /// �Ƴ���ָ����ν���������������ƥ�������Ԫ��
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
        /// �� List ���Ƴ�һ����Χ��Ԫ��
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
        /// ������ List ��Ԫ�ص�˳��ת
        /// </summary>
        public void Reverse()
        {
            list.Reverse();
        }

        /// <summary>
        /// ��ָ����Χ��Ԫ�ص�˳��ת
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void Reverse(int index, int count)
        {
            list.Reverse(index, count);
        }


        /// <summary>
        /// ʹ��Ĭ�ϱȽ��������� List �е�Ԫ�ؽ�������
        /// </summary>
        public void Sort()
        {
            list.Sort();
        }

        /// <summary>
        /// ʹ��ָ���� System.Comparison ������ List �е�Ԫ�ؽ�������
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<T> comparison)
        {
            list.Sort(comparison);
        }

        /// <summary>
        /// ��ָ���ıȽ��������� List �е�Ԫ�ؽ�������
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<T> comparer)
        {
            list.Sort(comparer);
        }


        /// <summary>
        /// ʹ��ָ���ıȽ����� List ��ĳ����Χ�ڵ�Ԫ�ؽ�������
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            list.Sort(index, count, comparer);
        }

        /// <summary>
        /// �� List ��Ԫ�ظ��Ƶ���������
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return list.ToArray();
        }

        /// <summary>
        /// ����������Ϊ List �е�ʵ��Ԫ����Ŀ���������ĿС��ĳ����ֵ��
        /// </summary>
        public void TrimExcess()
        {
            list.TrimExcess();
        }


        /// <summary>
        /// ȷ���Ƿ� List �е�ÿ��Ԫ�ض���ָ����ν���������������ƥ��
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool TrueForAll(Predicate<T> match)
        {
            return list.TrueForAll(match);
        }


        /// <summary>
        /// ��ȡ�����ø��ڲ����ݽṹ�ڲ�������С��������ܹ������Ԫ������
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
        /// ��ʼ�� List �����ʵ������ʵ��Ϊ�ղ��Ҿ���Ĭ�ϳ�ʼ����
        /// </summary>
        public TrackedList()
        {
            list = new List<T>();
        }

        /// <summary>
        /// ��ʼ�� List �����ʵ������ʵ��������ָ�����ϸ��Ƶ�Ԫ�ز��Ҿ����㹻�����������������Ƶ�Ԫ��
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
