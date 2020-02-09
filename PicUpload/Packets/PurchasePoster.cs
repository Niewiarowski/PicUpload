using Interceptor.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicUpload.Packets
{
    [Packet(hash: 28710679559143473)]
    public class PurchasePoster
    {
        public int Id { get; }
    }
}
