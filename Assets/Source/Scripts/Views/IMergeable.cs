using System;

namespace MiniIT.Views
{
    public interface IMergeable
    {
        public Action<IMergeable, IMergeable> Merging { get; set; }
    }
}
