using Interceptor.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicUpload.Packets
{
    [Packet(hash: 28147708127740005)]
    public class PreviewPoster
    {
        public byte[] CompressedData { get; set; }
    }
}
