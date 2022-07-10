using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivaModManager
{
    public static class StringConverters
    {
        static UI.i18n.i18n translationLoader = new UI.i18n.i18n();
        public static string FormatFileName(string filename)
        {
            return Path.GetFileName(filename);
        }
        // Load all suffixes in an array  
        static readonly string[] suffixes =
        { " Bytes", " KB", " MB", " GB", " TB", " PB" };
        public static string FormatSize(long bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1000) >= 1)
            {
                number = number / 1000;
                counter++;
            }
            return bytes != 0 ? string.Format("{0:n1}{1}", number, suffixes[counter])
                : string.Format("{0:n0}{1}", number, suffixes[counter]);
        }
        public static string FormatNumber(int number)
        {
            if (number > 1000000)
                return Math.Round((double)number / 1000000, 1).ToString() + "M";
            else if (number > 1000)
                return Math.Round((double)number / 1000, 1).ToString() + "K";
            else
                return number.ToString();
        }
        public static string FormatTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 60)
            {
                return Math.Floor(timeSpan.TotalMinutes).ToString() + translationLoader.GetTranslation("min");
            }
            else if (timeSpan.TotalHours < 24)
            {
                return Math.Floor(timeSpan.TotalHours).ToString() + translationLoader.GetTranslation("hr");
            }
            else if (timeSpan.TotalDays < 7)
            {
                return Math.Floor(timeSpan.TotalDays).ToString() + translationLoader.GetTranslation("d");
            }
            else if (timeSpan.TotalDays < 30.4)
            {
                return Math.Floor(timeSpan.TotalDays / 7).ToString() + translationLoader.GetTranslation("wk");
            }
            else if (timeSpan.TotalDays < 365.25)
            {
                return Math.Floor(timeSpan.TotalDays / 30.4).ToString() + translationLoader.GetTranslation("mo");
            }
            else
            {
                return Math.Floor(timeSpan.TotalDays % 365.25).ToString() + translationLoader.GetTranslation("yr");
            }
        }
        public static string FormatTimeAgo(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 60)
            {
                var minutes = Math.Floor(timeSpan.TotalMinutes);
                return minutes > 1 ? $"{minutes} {translationLoader.GetTranslation("minutes ago")}" : $"{minutes} {translationLoader.GetTranslation("minute ago")}";
            }
            else if (timeSpan.TotalHours < 24)
            {
                var hours = Math.Floor(timeSpan.TotalHours);
                return hours > 1 ? $"{hours} {translationLoader.GetTranslation("hours ago")}" : $"{hours} {translationLoader.GetTranslation("hour ago")}";
            }
            else if (timeSpan.TotalDays < 7)
            {
                var days = Math.Floor(timeSpan.TotalDays);
                return days > 1 ? $"{days} {translationLoader.GetTranslation("days ago")}" : $"{days} {translationLoader.GetTranslation("day ago")}";
            }
            else if (timeSpan.TotalDays < 30.4)
            {
                var weeks = Math.Floor(timeSpan.TotalDays / 7);
                return weeks > 1 ? $"{weeks} {translationLoader.GetTranslation("weeks ago")}" : $"{weeks}{translationLoader.GetTranslation("week ago")}";
            }
            else if (timeSpan.TotalDays < 365.25)
            {
                var months = Math.Floor(timeSpan.TotalDays / 30.4);
                return months > 1 ? $"{months} {translationLoader.GetTranslation("months ago")}" : $"{months} {translationLoader.GetTranslation("months ago")}";
            }
            else
            {
                var years = Math.Floor(timeSpan.TotalDays / 365.25);
                return years > 1 ? $"{years} {translationLoader.GetTranslation("years ago")}" : $"{years} {translationLoader.GetTranslation("year ago")}";
            }
        }
        public static string FormatSingular(string rootCat, string cat)
        {
            if (rootCat == null)
            {
                if (cat.EndsWith("es"))
                    return cat.Substring(0, cat.Length - 2);
                return cat.TrimEnd('s');
            }
            rootCat = rootCat.Replace("User Interface", "UI");

            if (cat == "Skin Packs")
                return cat.Substring(0, cat.Length - 1);

            if (rootCat.EndsWith("es"))
            {
                if (cat == rootCat)
                    return rootCat.Substring(0, rootCat.Length - 2);
                else
                    return $"{cat} {rootCat.Substring(0, rootCat.Length - 2)}";
            }
            else if (rootCat[rootCat.Length - 1] == 's')
            {
                if (cat == rootCat)
                    return rootCat.Substring(0, rootCat.Length - 1);
                else
                    return $"{cat} {rootCat.Substring(0, rootCat.Length - 1)}";
            }
            else
            {
                if (cat == rootCat)
                    return rootCat;
                else
                    return $"{cat} {rootCat}";
            }
        }
    }
}
