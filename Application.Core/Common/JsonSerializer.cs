using System.Text;

namespace Application.Core.Common
{
    public static class JsonSerializer
    {
        public static TValue? Deserialize<TValue>(string json)
        {
            json = json.Remove(0, 1);
            json = json.Remove(json.Length - 1);
            json = json.Replace("\t", "");

            var properties = typeof(TValue).GetProperties();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var newlyCreatedObject = (TValue)Activator.CreateInstance(typeof(TValue));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            string subObjectName = string.Empty;
            bool subObjectCreationFlag = false;

            // addressline : wkapwkap,}
            foreach (var commaSplitedItem in json.Split(','))
            {
                var removedWhiteSpaces = commaSplitedItem.Replace(" ", "");

                string subjsonPropertyName = string.Empty;
                string subjsonValue = string.Empty;

                string subjsonClosingPropertyName = string.Empty;
                string subjsonClosingValue = string.Empty;

                string jsonPropertyName = string.Empty;
                string jsonValue = string.Empty;

                switch (removedWhiteSpaces)
                {
                    case var x when removedWhiteSpaces.Contains("{"):

                        var subObject = removedWhiteSpaces.Split(':');

                        subObjectName = subObject[0];    //class name
                        subjsonPropertyName = subObject[1].Replace("{", "");
                        subjsonValue = subObject[2].Replace("'", "");

                        break;

                    case var y when removedWhiteSpaces.Contains("}"):

                        var subObjectValuePair = removedWhiteSpaces.Split(':');

                        subjsonClosingPropertyName = subObjectValuePair[0];
                        if (subObjectValuePair.Count() == 1)
                            continue;
                        subjsonClosingValue = subObjectValuePair[1].ToString().Replace("'", "");
                        subjsonClosingValue = subjsonClosingValue.Replace("}", "");

                        break;

                    default:

                        var propertyValuePair = removedWhiteSpaces.Split(':');

                        jsonPropertyName = propertyValuePair[0].ToLower();
                        jsonValue = propertyValuePair[1].Replace("'", "");

                        break;
                }

                foreach (var property in properties)
                {
                    var propertyName = property.Name.ToLower();

                    switch (propertyName)
                    {
                        case var x when propertyName.Equals(jsonPropertyName):

                            if (property.PropertyType == typeof(string))
                                property.SetValue(newlyCreatedObject, jsonValue);
                            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
                                property.SetValue(newlyCreatedObject, long.Parse(jsonValue));

                            break;

                        case var y when propertyName.Equals(subObjectName) && !subObjectCreationFlag:

                            var type = property.PropertyType;
                            var subObject = Activator.CreateInstance(type);
                            var subObjectProperties = subObject.GetType().GetProperties();

                            SetValuesToProperties(subjsonPropertyName, subjsonValue, subObject, subObjectProperties);
                            property.SetValue(newlyCreatedObject, subObject);
                            subObjectCreationFlag = true;

                            break;

                        case var z when propertyName.Equals(subObjectName) && subObjectCreationFlag:

                            var subObjectValue = property.GetValue(newlyCreatedObject);
                            var subClosingObjectProperties = subObjectValue.GetType().GetProperties();

                            SetValuesToProperties(jsonPropertyName, jsonValue, subObjectValue, subClosingObjectProperties);
                            subObjectName = String.Empty;

                            break;
                    }
                }
            }
            return newlyCreatedObject;
        }

        public static string Serialize<TValue>(TValue value)
        {
            StringBuilder jsonValue = new StringBuilder();

            jsonValue.Append("{");

            var myListElementType = value.GetType().GetGenericArguments().Single();

            foreach (var property in myListElementType.GetProperties())
            {
                if (property.GetValue(myListElementType) != null)
                {
                    jsonValue.Append($"{property.Name.ToLower()}");
                    jsonValue.Append(":");
                    jsonValue.Append(property.GetValue(value).ToString());
                    jsonValue.Append(",");
                }
            }

            jsonValue.Append("}");

            return jsonValue.ToString();
        }

        private static void SetValuesToProperties(string subjsonPropertyName, string subjsonValue, object? subObject, System.Reflection.PropertyInfo[] subObjectProperties)
        {
            foreach (var subProeprty in from subProeprty in subObjectProperties
                                        where subProeprty.Name.ToLower().Equals(subjsonPropertyName.ToLower())
                                        select subProeprty)
            {
                if (subProeprty.PropertyType == typeof(string))
                    subProeprty.SetValue(subObject, subjsonValue);
                else if (subProeprty.PropertyType == typeof(long) || subProeprty.PropertyType == typeof(long?))
                    subProeprty.SetValue(subObject, long.Parse(subjsonValue));
                break;
            }
        }
    }
}
