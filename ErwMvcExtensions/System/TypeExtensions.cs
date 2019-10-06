using System;

namespace ErwMvcExtensions.System
{
    public static class TypeExtensions
    {
        public static object ParseIfPrimitive(this Type valueType, object value)
        {
            object parsedValue = null;

            if (!valueType.IsPrimitive)
            {
                return parsedValue;
            }

            try
            {
                switch (Type.GetTypeCode(valueType))
                {
                    case TypeCode.Empty:
                        parsedValue = null;
                        break;
                    case TypeCode.Object:
                        parsedValue = (object)value;
                        break;
                    case TypeCode.DBNull:
                        parsedValue = (DBNull)value;
                        break;
                    case TypeCode.Boolean:
                        parsedValue = bool.Parse(value.ToString());
                        break;
                    case TypeCode.Char:
                        parsedValue = Convert.ToChar(value.ToString());
                        break;
                    case TypeCode.SByte:
                        parsedValue = sbyte.Parse(value.ToString());
                        break;
                    case TypeCode.Byte:
                        parsedValue = byte.Parse(value.ToString());
                        break;
                    case TypeCode.Int16:
                        parsedValue = short.Parse(value.ToString());
                        break;
                    case TypeCode.UInt16:
                        parsedValue = ushort.Parse(value.ToString());
                        break;
                    case TypeCode.Int32:
                        parsedValue = int.Parse(value.ToString());
                        break;
                    case TypeCode.UInt32:
                        parsedValue = uint.Parse(value.ToString());
                        break;
                    case TypeCode.Int64:
                        parsedValue = long.Parse(value.ToString());
                        break;
                    case TypeCode.UInt64:
                        parsedValue = ulong.Parse(value.ToString());
                        break;
                    case TypeCode.Single:
                        parsedValue = float.Parse(value.ToString());
                        break;
                    case TypeCode.Double:
                        parsedValue = double.Parse(value.ToString());
                        break;
                    case TypeCode.Decimal:
                        parsedValue = decimal.Parse(value.ToString());
                        break;
                    case TypeCode.DateTime:
                        parsedValue = DateTime.Parse(value.ToString());
                        break;
                    case TypeCode.String:
                        parsedValue = value.ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return parsedValue;
            }

            return parsedValue;
        }
    }
}
