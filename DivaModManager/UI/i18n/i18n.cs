using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DivaModManager.UI.i18n
{
    public class i18n
    {
        public static Dictionary<string, ResourceDictionary> dictionaries;

        public string CurrentLanguage { get; set; } = "en";

        static i18n()
        {
            dictionaries = Application.Current.Resources.MergedDictionaries.Where(x => x.Source != null).ToDictionary(x => x.Source.OriginalString);
        }

        public string GetLanguage(string targetLanguage) => GetLanguage(CultureInfo.CreateSpecificCulture(targetLanguage));

        public string GetLanguage() => GetLanguage(CultureInfo.CurrentUICulture);

        public string GetLanguage(CultureInfo cultureInfo)
        {
            do
            {
                var targetLanguage = cultureInfo.Name;

                if (SupportedLanguages.ContainsValue(targetLanguage))
                {
                    return targetLanguage;
                }
            } while ((cultureInfo = cultureInfo.Parent).Name != ""); // fallback to parent culture (zh-CN,zh-Hans,zh) until InvariantCulture ""

            return "en";
        }

        public void UpdateUserInterfaceLanguage() => UpdateUserInterfaceLanguage(GetLanguage());

        public void UpdateUserInterfaceLanguage(string targetLanguage)
        {
            if (!SupportedLanguages.Values.Contains(targetLanguage))
                throw new ArgumentOutOfRangeException(nameof(targetLanguage));

            ResourceDictionary rd = dictionaries[$@"UI\i18n\Translation.{targetLanguage}.xaml"];

            if (targetLanguage == CurrentLanguage)
                return;

            // put target language's dictionary at last of the merged dictionaries
            Application.Current.Resources.MergedDictionaries.Remove(rd);
            Application.Current.Resources.MergedDictionaries.Add(rd);

            CurrentLanguage = targetLanguage;
        }

        public string GetTranslation(string requestString)
        {
            return Application.Current.FindResource(requestString).ToString();
        }

        public Dictionary<string, string> SupportedLanguages = new Dictionary<string, string>
        {
            { "English", "en" },
            { "简体中文" , "zh-CN" }
        };
    }
}
