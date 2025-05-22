using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.BLL.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr != null ? attr.Description : value.ToString();
        }

        public static T? ParseFromDescription<T>(string description) where T : struct
        {
            foreach (var field in typeof(T).GetFields())
            {
                var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if ((attr != null && attr.Description == description) || field.Name == description)
                    return (T)field.GetValue(null);
            }
            return null;
        }

        public static void BindEnumToComboBox<TEnum>(ComboBox comboBox) where TEnum : Enum
        {
            comboBox.DataSource = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new KeyValuePair<TEnum, string>(e, EnumHelper.GetDescription(e)))
                .ToList();
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
        }
    }
}
