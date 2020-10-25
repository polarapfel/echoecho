using System;
using System.IO;
using System.Net;

namespace EchoEcho.Common
{
    public class EchoClientOptions
    {
        private Protocol Protocol { get; set; } = Protocol.TCP;
        private IPVersion IpVersion { get; set; } = IPVersion.IPv4;
        private int BlockSize { get; set; } = 1024; // bytes
        private int Delay { get; set; } = 0; // micro seconds
        private Stream DataSource { get; set; } = new RandomByteStream();
        private int BlockCount { get; set; } = 100;
        private IPEndPoint Address { get; set; } // has IP and port, does support IPv4 and IPv6

        public EchoClientOptions(IPEndPoint address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }

    enum Protocol
    {
        TCP = 0,
        UDP = 1
    }

    enum IPVersion
    {
        IPv4 = 4,
        IPv6 = 6
    }
}
