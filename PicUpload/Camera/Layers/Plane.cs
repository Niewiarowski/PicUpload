using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PicUpload.Camera.Layers
{
    [DataContract(Name = "plane")]
    public class Plane
    {
        private List<Point> _cornerPoints;
        [DataMember(Name = "cornerPoints", Order = 2)]
        public List<Point> CornerPoints
        {
            get { return _cornerPoints ?? (_cornerPoints = new List<Point>()); }
        }

        [DataMember(Name = "bottomAligned", Order = 4)]
        public bool IsBottomAligned { get; set; }

        private object[] _masks;
        [DataMember(Name = "masks", Order = 5, EmitDefaultValue = false)]
        public object[] Masks { get => _masks; set => _masks = value; }

        private object[] _texCols = new object[0];
        [DataMember(Name = "texCols", Order = 3)]
        public object[] TexCols { get => _texCols; set => _texCols = value; }

        [DataMember(Name = "z", Order = 1)]
        public double Z { get; set; }

        [DataMember(Name = "color", Order = 0)]
        public int Color { get; set; }
    }
}