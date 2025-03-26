using System.ComponentModel;
using System.Data;

namespace SimpleETL.Extensions;

public static class ListExtensions
{
    public static DataTable ToDataTable<T>(this IList<T> data)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();

        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            Type propType = prop.PropertyType;

            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propType = Nullable.GetUnderlyingType(propType)!;
            }

            table.Columns.Add(prop.Name, propType);
        }

        foreach (T item in data)
        {
            object[] values = new object[props.Count];
            for (int i = 0; i < values.Length; i++)
            {
                object? value = props[i].GetValue(item);
                values[i] = value ?? DBNull.Value;
            }
            table.Rows.Add(values);
        }

        return table;     
    }
}
