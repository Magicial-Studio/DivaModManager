using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DivaModManager.UI.i18n
{
    internal class i18n
    {
        public List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
        public string DefaultLanguage { get => System.Globalization.CultureInfo.CurrentCulture.Name; }
        
        public void UpdateUserInterfaceLanguage(string targetLanguage=null)
        {
            if (targetLanguage == null)
            {
                targetLanguage= DefaultLanguage;
            }
            else if (targetLanguage.StartsWith("en"))
            {
                targetLanguage = null;
            }
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = @"UI\i18n\Translation" + targetLanguage + ".xaml";
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            
        }
        public string GetTranslation(string message)
        {
            return Application.Current.FindResource(message).ToString();
        }
    }
    public static class SupportedLanguages 
    { 
        public static string[] supportedLanguages = { "en" };
        public static string[] supportedLanguagesFriendlyName = { "English" };
    }
    
}
