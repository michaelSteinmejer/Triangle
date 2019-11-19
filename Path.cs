using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleOddEvenSum
{
    class Path : ICloneable
    {
        // Sum af path
        public int sum { get; set; }

        // Nuværende niveau i trekanten
        public int level { get; set; }

        // list af indexes i alle nodes i pathen
        public List<int> path { get; set; }

        public Path()
        {
            sum = 0;
            level = 0;
            path = new List<int>();
        }

       
        public Path(int root)
        {
            sum = root;
            level = 0;
            path = new List<int>();
            path.Add(0);
        }

        object ICloneable.Clone()
        {
            Path clone = new Path();
            clone.level = level;
            clone.sum = sum;
            // klon hver node i pathen
            List<int> pathClone = new List<int>();
            foreach (int index in path) pathClone.Add(index);
            clone.path = pathClone;

            return clone;
        }

        //returner index af parent node
        public int getParentIndex()
        {
            return path.Count > 0 ? path[path.Count - 1] : -1;
        }
    }
}
