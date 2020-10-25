using System;
using System.IO;

namespace EchoEcho.Common
{
    public class RandomByteStream : Stream
    {
        
        private Random _rnd = new Random();

        public override void Flush()
        {
            _rnd = new Random();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 ) throw new ArgumentOutOfRangeException("offset");
            if (count < 0 ) throw new ArgumentOutOfRangeException("count");
            if (buffer.Length - offset < count) throw new ArgumentException("Array does not hold enough elements.", "buffer");
            _rnd.NextBytes(new Span<byte>(buffer, offset, count));
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotSupportedException();
        }

        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = false;
        public override bool CanWrite { get; } = false;
        public override long Length { get; } = -1;
        public override long Position { get; set; }
    }
}