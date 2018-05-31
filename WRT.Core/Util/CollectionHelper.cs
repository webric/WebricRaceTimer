using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

namespace WRT.Core.Util
{
    /// <summary>
    /// Lmbda exempel:  temp = result.FindAll(li => li.LyricId == l.LyricId);
    /// </summary>
    public static class CollectionHelper
    {
        /// <summary>
        /// Ej testad
        /// Användning: DataTable dt = Webric.WCore.BLL.CollectionHelper.ConvertTo(malList);
        /// </summary>
        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }
        /// <summary>
        /// Ej testad
        /// Användning: DataTable dt = Webric.WCore.BLL.CollectionHelper.ConvertTo(malList);
        /// </summary>
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }
        /// <summary>
        /// Skapar upp en datatable med samma struktur och data
        /// Användning: DataTable dt = Webric.WCore.BLL.CollectionHelper.ConvertTo(malList);
        /// </summary>
        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
                return null;

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
                rows.Add(row);

            return ConvertTo<T>(rows);
        }
        /// <summary>
        /// Ej testad
        /// </summary>
        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch (Exception ex)
                    {
                        LoggHelper.Error("Kunde inte slutföra CreateItem<T>", LoggHelper.State.Other, (new System.Diagnostics.StackFrame()).GetMethod().Name, ex);
                    }
                }
            }

            return obj;
        }
        /// <summary>
        /// Skapar upp en tabell med "samma" struktur som objektet i listan
        /// Användning: DataTable table = CreateTable<listOfOrderRows>();
        /// </summary>
        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    NullableConverter converter = new NullableConverter(prop.PropertyType);
                    table.Columns.Add(prop.Name, converter.UnderlyingType);
                }
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
        /// <summary>
        /// Gör om en enum till en generisk lista
        /// </summary>
        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));

            return enumValList;
        }
        /// <summary>
        /// Plockar bort dubletter från en lista
        /// usage: TNGUI, ADGUI, MEAGUI
        /// </summary>
        public static T[] GetDistinctValues<T>(T[] array)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                if (tmp.Contains(array[i]))
                    continue;
                tmp.Add(array[i]);
            }

            return tmp.ToArray();
        }
        public static void SetSelectedByValue(ListControl lctr, Object value)
        {
            SetSelectedByValue(lctr, value, String.Empty);
        }
        public static void SetSelectedByValue(ListControl lctr, Object value, String errorMessage)
        {
            if (lctr.Items.Count > 0)
            {
                try
                {
                    lctr.ClearSelection();
                    lctr.Items.FindByValue(ConversionHelper.toStr(value)).Selected = true;
                }
                catch 
                {
                }
            }
        }
        public static void SetSelectedByText(ListControl lctr, String value, String errorMessage)
        {
            try
            {
                lctr.ClearSelection();
                lctr.Items.FindByText(value).Selected = true;
            }
            catch
            {
            }
        }
        public static void SetSelectedByIndex(ListControl lctr, int index)
        {
            SetSelectedByIndex(lctr, index, String.Empty);
        }
        public static void SetSelectedByIndex(ListControl lctr, int index, String errorMessage)
        {
            try
            {
                lctr.ClearSelection();
                if (lctr.Items.Count > 0)
                {
                    lctr.SelectedIndex = index;
                }
            }
            catch (Exception )
            {
            }
        }
        public static String GetAttributeAsStringInListOfRows(List<Object> objList, String attr, String separator)
        {
            StringBuilder sb = new StringBuilder();
            if (attr.Contains("."))
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    String[] arr = attr.Split('.');
                    String value = (String)GetPropertyInChildObject(objList[i], arr);
                    if (value != null)
                    {
                        sb.Append(value);
                        if (i < objList.Count - 1)
                        {
                            sb.Append(separator);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    String value = (String)GetProperty(objList[i], attr);
                    if (value != null)
                    {
                        sb.Append(value);
                        if (i < objList.Count - 1)
                        {
                            sb.Append(separator);
                        }
                    }
                }
            }
            return sb.ToString();
        }
        private static Object GetProperty(Object obj, String attr)
        {
            Object ret = null;
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(obj)[attr];
            if (propDesc != null)
            {
                ret = propDesc.GetValue(obj);
            }
            return ret;
        }
        private static Object GetPropertyInChildObject(Object obj, String[] attr)
        {
            Object ret = null;
            if (attr.Length > 1)
            {
                String[] newArr = new String[attr.Length - 1];
                for (int i = 1; i < attr.Length; i++)
                {
                    newArr[i - 1] = attr[i];
                }
                obj = GetPropertyInChildObject(GetProperty(obj, attr[0]), newArr);
                ret = obj;
            }
            else
            {
                ret = GetProperty(obj, attr[0]);
            }
            return ret;
        }
        /// <summary>
        /// Returnerar List<string> från listbox om flera rader är valda (selectionmode: multiple)
        /// </summary>
        public static List<string> SelectedValues(this ListBox lbox)
        {
            List<string> selectedValues = new List<string>();

            int[] selectedIndeces = lbox.GetSelectedIndices();

            foreach (int i in selectedIndeces)
                selectedValues.Add(lbox.Items[i].Value);

            return selectedValues;
        }
        /// <summary>
        /// Select multiple items in a ListBox
        /// </summary>
        public static void SelectedValues(this ListBox lbox, string[] values)
        {
            foreach (string value in values)
            {
                ListItem item = lbox.Items.FindByValue(value);
                if (item != null)
                    item.Selected = true;
            }
        }
        /// <summary>
        /// http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_23645179.html
        /// TESTA!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public static List<Type> removeDuplicates(List<Type> listWithDuplicates)
        {
            List<Type> listDistinct = new List<Type>();
            foreach (Type t in listWithDuplicates)
            {
                if (!listDistinct.Contains(t))
                    listDistinct.Add(t);
            }
            return listDistinct;
        }
        /// <summary>
        /// Sorterar en lista
        /// http://www.codeproject.com/KB/cs/GenericSorter.aspx
        /// http://www.codeproject.com/KB/linq/Article.aspx?aid=27834
        /// </summary>
        /// <param name="list">Listan som ska sorteras</param>
        /// <param name="sortExpression">Skriv enligt "Birthday asc, Firstname asc";
        /// (@param1 [sortdirection], @param2 [sortdirection] osv.)
        /// Valid sortDirections are: asc, desc, ascending and descending.</param>
        public static void Sort<T>(this List<T> list, string sortExpression)
        {
            string[] sortExpressions = sortExpression.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            List<GenericComparer> comparers = new List<GenericComparer>();

            foreach (string sortExpress in sortExpressions)
            {
                string sortProperty = sortExpress.Trim().Split(' ')[0].Trim();
                string sortDirection = sortExpress.Trim().Split(' ')[1].Trim();

                Type type = typeof(T);
                PropertyInfo PropertyInfo = type.GetProperty(sortProperty);
                if (PropertyInfo == null)
                {
                    PropertyInfo[] props = type.GetProperties();

                    foreach (PropertyInfo info in props)
                    {
                        if (info.Name.ToString().ToLower() == sortProperty.ToLower())
                        {
                            PropertyInfo = info;
                            break;
                        }
                    }
                    if (PropertyInfo == null)
                        throw new Exception(String.Format("{0} is not a valid property of type: \"{1}\"", sortProperty, type.Name));
                }

                SortDirection SortDirection = SortDirection.Ascending;
                if (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "ascending")
                    SortDirection = SortDirection.Ascending;
                else if (sortDirection.ToLower() == "desc" || sortDirection.ToLower() == "descending")
                    SortDirection = SortDirection.Descending;
                else
                    throw new Exception("Valid SortDirections are: asc, ascending, desc and descending");

                comparers.Add(new GenericComparer
                {
                    SortDirection = SortDirection,
                    PropertyInfo = PropertyInfo,
                    comparers = comparers
                });
            }

            list.Sort(comparers[0].Compare);
        }
        /// <summary>
        /// http://dotnetperls.com/sort-dictionary-values
        /// </summary>
        /// <param name="?"></param>
        /// <param name="sortByValue"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        //public static IOrderedEnumerable<TElement> SortByValue(this Dictionary<TKey, T> d, bool sortByValue, bool sortAscending)
        //{
        //    var items;
        //if(sortByValue && sortAscending)
        //    items=  from k in d.Values
        //            orderby d[k] ascending
        //            select k;

        //return items;
        //}
        //public static List<T> SortByValue(this Dictionary<TKey, TValue> dict)
        //{
        //    // Get list of keys.
        //    List<TKey> keys = dict.Keys.ToList;

        //    // Sort the keys.
        //    keys.Sort();

        //    // Loop over the sorted keys.
        //    Dictionary<TKey, TValue> back = new Dictionary<TKey, TValue>();
        //    foreach (TKey key in keys)
        //        back.Add(key, dict[key]);

        //    return back;
        //}
        public class GenericComparer
        {
            public List<GenericComparer> comparers { get; set; }
            int level = 0;

            public SortDirection SortDirection { get; set; }
            public PropertyInfo PropertyInfo { get; set; }

            public int Compare<T>(T t1, T t2)
            {
                int ret = 0;

                if (level >= comparers.Count)
                    return 0;

                object t1Value = comparers[level].PropertyInfo.GetValue(t1, null);
                object t2Value = comparers[level].PropertyInfo.GetValue(t2, null);

                if (t1 == null || t1Value == null)
                    if (t2 == null || t2Value == null)
                        ret = 0;
                    else
                        ret = -1;
                else
                    if (t2 == null || t2Value == null)
                        ret = 1;
                    else
                        ret = ((IComparable)t1Value).CompareTo(((IComparable)t2Value));
                if (ret == 0)
                {
                    level += 1;
                    ret = Compare(t1, t2);
                    level -= 1;
                }
                else
                    if (comparers[level].SortDirection == SortDirection.Descending)
                        ret *= -1;

                return ret;
            }
        }
        //        /// <summary>
        //        /// http://stackoverflow.com/questions/47752/remove-duplicates-from-a-listt-in-c
        //        /// </summary>
        //        /// <param name="list"></param>
        //        /// <returns></returns>
        //        public static IList<T> RemoveDuplicates(IList<T> list)
        //        {
        //            return list.Distinct(.Distinct().ToList();

        //        }
        //        // TODO
        //        // TODO
        //        /// <summary>
        //        /// http://social.msdn.microsoft.com/Forums/en-US/Vsexpressvcs/thread/eb008d1e-d2d8-46cc-8b48-05909619a214
        //        /// </summary>
        //        /// <param name="inputList"></param>
        //        /// <returns></returns>
        //        public static List<Info> RemoveDuplicates(List<Info> inputList)
        //        {
        //            MyComparer mc = new MyComparer();
        //            List<Info> finalList = new List<Info>();
        //            foreach (Info currValue in inputList)
        //            {
        //                if (!finalList.Contains(currValue, mc))
        //                    finalList.Add(currValue);
        //            }
        //            return finalList;
        //        }

        //        class MyComparer : IEqualityComparer<Info>
        //        {
        //            public bool Equals(Info x, Info y)
        //            {
        //                return (x.ID == y.ID);
        //            }

        //            public int GetHashCode(Info obj)
        //            {
        //                return obj.GetHashCode();
        //            }
        //        }

        //        public static IList<T> RemoveDuplicates<T>(this List<T> list, Comparison<T> comparison)
        //        {
        //            for (int i = 0; i < list.Count; i++)
        //            {
        //                for (int j = list.Count-1; j > i; j=j-1)
        //                {
        //                    if (comparison(list[i], list[j]) == 0)
        //                    {
        //                        list.RemoveAt(j);
        //                    }
        //                }
        //            }
        //        }
        //List<Person> persons = new List<Person>(); 
        //persons.Add( new Person("Tom",30) ); 
        //persons.Add(new Person("Harry", 55)); 
        //// sort in ascending order 
        //persons.Sort( delegate(Person person0, Person person1)
        //{ 
        //    return person0.FirstName.CompareTo(person1.FirstName); 
        //} 
        //); 

        //// sort in descending order 
        //persons.Sort( delegate(Person person0, Person person1) 
        //{ 
        //    return person1.FirstName.CompareTo(person0.FirstName); 
        //} 
        //);
    }
}
