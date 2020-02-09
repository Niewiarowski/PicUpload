using Interceptor.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicUpload.Packets
{
    [Packet(hash: 15762813447634994)]
    public class MovePoster
    {
        public int Id { get; set; }
        public string Position { get; set; }
    }
}
