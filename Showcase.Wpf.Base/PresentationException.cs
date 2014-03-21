using System;
using System.Runtime.Serialization;

namespace Showcase.Wpf.Base
{
    [Serializable]
    public class PresentationException : Exception
    {
        public PresentationException() { }

        public PresentationException(string message)
            : base(message) { }

        public PresentationException(string message, Exception inner)
            : base(message, inner) { }

        protected PresentationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}