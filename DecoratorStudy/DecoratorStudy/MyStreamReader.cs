﻿namespace DecoratorStudy
{
    using System;
    using System.IO;
    using System.Text;

    public class MyStreamReader : IDisposable
    {
        private Stream _stream;
        private Encoding _encoding;
        private bool _closable;

        public MyStreamReader(Stream stream, Encoding encoding)
        {
            this._stream = stream;
            this._encoding = encoding;
        }

        public MyStreamReader(string filePath, Encoding encoding)
        {
            this._stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                1024,
                FileOptions.SequentialScan);

            this._encoding = encoding;
            this._closable = true;
        }

        internal bool LeaveOpen
        {
            get { return !this._closable; }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public string ReadToEnd()
        {
            var binary = new byte[this._stream.Length];
            this._stream.Read(binary, 0, binary.Length);
            return this._encoding.GetString(binary);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.LeaveOpen && isDisposing && (this._stream != null))
                {
                    this._stream.Close();
                }
            }
            finally
            {
                if (!this.LeaveOpen && (this._stream != null))
                {
                    this._stream = null;
                    this._encoding = null;
                }
            }
        }
    }
}
