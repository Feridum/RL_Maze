using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class GridTranslation
    {
        string gridName;
        Translation translation;
        public static string[] avaiableGridNames = new []{ "main", "transition" };

        public GridTranslation(string gridName, Translation translation)
        {
            this.gridName = gridName;
            this.translation = translation;
        }

        public Translation getTranslation()
        {
            return translation;
        }

        public string getName()
        {
            return gridName;
        }
    }
}
