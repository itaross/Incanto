using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Incanto.Helpers
{
    public class ControlsFinder
    {
        private static StringBuilder childrenTreeInfo;
        public static StringBuilder ChildrenTreeInfo
        {
            get
            {
                return childrenTreeInfo != null ? childrenTreeInfo : new StringBuilder();
            }

            private set { }
        }


        public static List<T> GetLogicalChildren<T>(object parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            childrenTreeInfo = new StringBuilder();
            GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
            return logicalCollection;
        }

        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T && child != null)
                    {
                        logicalCollection.Add(child as T);
                        childrenTreeInfo.Append(child.GetType());
                    }

                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

    }
}
