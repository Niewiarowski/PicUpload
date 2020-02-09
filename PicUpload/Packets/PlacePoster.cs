using Interceptor.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicUpload.Packets
{
    [Packet(hash: 13511219792117859)]
    public class PlacePoster
    {
        public string Position { get; set; }
    }
}
