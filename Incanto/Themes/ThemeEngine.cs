﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Incanto.Themes
{
    /*
        This class allows to manage multiple themes, using the DynamicResource principle.
    */
    public class ThemeEngine : DependencyObject
    {
        public static readonly DependencyProperty CurrentThemeDictionaryProperty =
         DependencyProperty.RegisterAttached("CurrentThemeDictionary", typeof(Uri),
         typeof(ThemeEngine),
         new UIPropertyMetadata(null, CurrentThemeDictionaryChanged));

        public static Uri GetCurrentThemeDictionary(DependencyObject obj)
        {
            return (Uri)obj.GetValue(CurrentThemeDictionaryProperty);
        }

        public static void SetCurrentThemeDictionary(DependencyObject obj, Uri value)
        {
            obj.SetValue(CurrentThemeDictionaryProperty, value);
        }

        public static void CurrentThemeDictionaryChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is FrameworkElement) // works only on FrameworkElement objects
            {
                ApplyTheme(obj as FrameworkElement, GetCurrentThemeDictionary(obj));
            }
        }

        private static void ApplyTheme(FrameworkElement targetElement, Uri dictionaryUri)
        {
            if (targetElement == null) return;

            try
            {
                ThemeResourceDictionary themeDictionary = null;
                if (dictionaryUri != null)
                {
                    themeDictionary = new ThemeResourceDictionary();
                    themeDictionary.Source = dictionaryUri;

                    // add the new dictionary to the collection of merged dictionaries of the target object
                    targetElement.Resources.MergedDictionaries.Insert(0, themeDictionary);
                }

                // find if the target element already has a theme applied
                List<ThemeResourceDictionary> existingDictionaries =
                    (from dictionary in targetElement.Resources.MergedDictionaries.OfType<ThemeResourceDictionary>()
                     select dictionary).ToList();

                // remove the existing dictionaries
                foreach (ThemeResourceDictionary thDictionary in existingDictionaries)
                {
                    if (themeDictionary == thDictionary) continue;  // don't remove the newly added dictionary
                    targetElement.Resources.MergedDictionaries.Remove(thDictionary);
                }
            }
            finally { }
        }
    }
}